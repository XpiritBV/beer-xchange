using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Parsers.Markdown;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange.Services
{
    public class BeerService : IBeerService
    {
        private readonly BeerXchangeContext context;
        private readonly IOpenAIService openAiService;

        public BeerService(BeerXchangeContext context, IOpenAIService openAiService)
        {
            this.context = context;
            this.openAiService = openAiService;
        }

        public async Task AddBeer(Beer beer)
        {
            context.Beer.Add(beer);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Beer>> GetAllBeers()
        {
            return await context.Beer.ToListAsync();
        }






        public async Task<Beer> GetBeerById(int id)
        {
            return await context.Beer.SingleAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Beer>> GetBeerHistory()
        {
            return await context.Beer.Where(b => b.RemovedDate != null || b.RemovedBy != null).OrderByDescending(b => b.RemovedDate).ToListAsync();
        }

        public async Task<IEnumerable<Beer>> GetCurrentBeers()
        {
            return await context.Beer.Where(b => b.RemovedBy == null).ToListAsync();
        }

        public async Task UpdateBeer(Beer beer)
        {
            if (beer is null)
            {
                throw new ArgumentNullException(nameof(beer));
            }

            var existingBeer = await context.Beer.SingleOrDefaultAsync(b => b.Id == beer.Id);
            if (existingBeer is null)
            {
                throw new ArgumentException(nameof(beer), $"Beer with Id {beer.Id} does not exist");
            }

            context.Update(beer);
            await context.SaveChangesAsync();
        }

        public async Task<int> GetUserCredits(string user)
        {
            var beers = await context.Beer.ToListAsync();
            var created = beers.Count(b => b.CreatedBy == user);
            var removed = beers.Count(b => b.RemovedBy == user);
            return created - removed;
        }

        public async Task<List<UserCredits>> GetAllUserCredits()
        {
            var beers = await context.Beer.ToListAsync();

            List<UserCredits> userCredits = new List<UserCredits>();
            foreach (var user in beers.Select(b => b.CreatedBy).Distinct())
            {
                var beersAdded = beers.Count(b => b.CreatedBy == user);
                var beersTaken = beers.Count(b => b.RemovedBy == user);

                UserCredits credit = new UserCredits()
                {
                    Name = user,
                    BeersAdded = beersAdded,
                    BeersTaken = beersTaken,
                    Credits = beersAdded - beersTaken
                };
                userCredits.Add(credit);
            }

            return userCredits.OrderBy(c => c.Name).ToList();
        }

        // GET API Method explain beer by id
        public async Task<string> GetBeerExplainById(int id)
        {
            var beer = await context.Beer.SingleAsync(b => b.Id == id);

            return await HoldMyBeer(beer);
        }

        public async Task<string> HoldMyBeer(Beer beer)
        {

            var systemPrompt =
"""
            I want to act as a Cicerone
            I will give you details about a beer and you will tell me some interesting facts about it
            It needs to be conficing, so that the user will buy it
""";

            var userPrompt = $"Name: {beer.Name}, Country: {beer.Country}, Brewery {beer.Brewery}";

            var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(systemPrompt),
                ChatMessage.FromUser(userPrompt)
            },
                Model = Models.ChatGpt3_5Turbo0301,

                // MaxTokens = 50
            });


            if (completionResult.Successful)
            {
                Console.WriteLine(completionResult.Choices.First().Message.Content);
            }

            var content = completionResult.Choices.First().Message.Content;
            MarkdownDocument markdownDocument = new MarkdownDocument();
            markdownDocument.Parse(content);

            return "";
        }
    }
}
