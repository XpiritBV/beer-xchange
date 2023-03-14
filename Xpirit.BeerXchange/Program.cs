using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.GPT3.Extensions;
using Xpirit.BeerXchange;
using Xpirit.BeerXchange.Services;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddOpenAIService(settings =>
{
    settings.ApiKey = "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";
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
