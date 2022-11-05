﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Reflection;

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
                movie = Movie.FromJson(movieSearchResult);
            }

            // tempdata is needed to store and pass the data to another cshtml UI page
            TempData.Put("Movie", movie);

            // redirecting to show search results in their own page
            Response.Redirect("/SeeMovieDetails");
        }
    }
}