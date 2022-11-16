﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Reflection;
using System.Text.Json.Nodes;

namespace IS7024WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
            string tmApiKey = config["tmApiKey"];
            ViewData["tmApiKey"] = tmApiKey;

            var task = client.GetAsync($"https://api.themoviedb.org/3/trending/movie/week?api_key={tmApiKey}");
            HttpResponseMessage result = task.Result;
            TrendingMovieModel trendingMovies = new TrendingMovieModel();
            if (result.IsSuccessStatusCode)
            {
                Task<string> readString = result.Content.ReadAsStringAsync();
                string jsonString = readString.Result;
                trendingMovies = TrendingMovieModel.FromJson(jsonString);
            }
            ViewData["trendingMovies"] = trendingMovies;
        }

        public void OnGetFormSubmit(string Movie_Title)
        {
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
            string movieSearchApikey = config["movieSearchApikey"];

            var task = client.GetAsync($"https://www.omdbapi.com/?apikey={movieSearchApikey}&t=" + Movie_Title);
            HttpResponseMessage result = task.Result;
            var movie = new Movie();
            if (result.IsSuccessStatusCode)
            {
                Task<string> readMovieString = result.Content.ReadAsStringAsync();
                string movieSearchResult = readMovieString.Result;

                // streaming search
                var taskStreaming = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://streaming-availability.p.rapidapi.com/search/basic?country=us&service=netflix&type=movie&language=en&keyword=" + Movie_Title),
                    Headers =
                {
                    { "X-RapidAPI-Key", "8306c392d2msha2f01fa02736a36p138bc7jsn920b2226f7d5" },
                    { "X-RapidAPI-Host", "streaming-availability.p.rapidapi.com" },
                },
                };


                // validation
                JSchema movieSchema = JSchema.Parse(System.IO.File.ReadAllText("Schemas/movie-schema.json"));
                JObject jsonObject = JObject.Parse(movieSearchResult);
                IList<string> validationEvents = new List<string>();
                if (jsonObject.IsValid(movieSchema, out validationEvents))
                {
                    movie = Movie.FromJson(movieSearchResult);
                }
                else
                {
                    foreach (string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                    }
                }
                
            }

            // temp to pass streaming availability
            var streamingavailability = new StreamingAvailability();
            TempData.Put("movie", streamingavailability);

            // tempdata is needed to store and pass the data to another cshtml UI page
            TempData.Put("Movie", movie);

            // redirecting to show search results in their own page
            Response.Redirect("/SeeMovieDetails");
        }
    }
}