using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;
using Xpirit.BeerXchange;
using Xpirit.BeerXchange.Services;

var builder = WebApplication.CreateBuilder(args);

// Set Simple kernel instance
var kernel = Kernel.Builder.Build();
kernel.Config.AddAzureOpenAICompletionBackend(
    "davinci-backend",                   // Alias used by the kernel
    "text-davinci-003",                  // Azure OpenAI *Deployment ID*
    "https://openai-aiprompts-dev.openai.azure.com/", // Azure OpenAI *Endpoint*
    "50c7d105d79546a4a0836326e82c6aa5"        // Azure OpenAI *Key*
);

builder.Services.AddSingleton(kernel);

builder.Services
    .AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
    .AddAzureADBearer(options => builder.Configuration.Bind("AzureActiveDirectory", options));

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddCors((options) =>
{
    options.AddPolicy("FrontEnd", builder => builder
            .SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            );
});

builder.Services.AddDbContext<BeerXchangeContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BeerXchangeContext")));

builder.Services.AddScoped<IBeerService, BeerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors("FrontEnd");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
