using Microsoft.AspNetCore.Mvc;
using GameReviewWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameReviewWebsite.Controllers
{
    public class GamesController : Controller
    {
        private List<Game> games = new List<Game>
        {
            new Game { Id = 1, Title = "Game 1", Genre = "Action", ReleaseDate = new DateTime(2021, 1, 1), Rating = 4.5, Description = "Exciting action game." },
            new Game { Id = 2, Title = "Game 2", Genre = "Adventure", ReleaseDate = new DateTime(2022, 5, 3), Rating = 4.2, Description = "Immersive adventure game." },
            new Game { Id = 3, Title = "Game 3", Genre = "Puzzle", ReleaseDate = new DateTime(2020, 3, 15), Rating = 4.8, Description = "Challenging puzzle game." }
        };

        public IActionResult List(string searchString, string genre, double? minRating, string sortOrder)
        {
            var filteredGames = games.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                filteredGames = filteredGames.Where(g => g.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            // Apply genre filter
            if (!string.IsNullOrEmpty(genre))
            {
                filteredGames = filteredGames.Where(g => g.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
            }

            // Apply rating filter
            if (minRating.HasValue)
            {
                filteredGames = filteredGames.Where(g => g.Rating >= minRating.Value);
            }

            filteredGames = sortOrder switch
            {
                "title_asc" => filteredGames.OrderBy(g => g.Title),
                "title_desc" => filteredGames.OrderByDescending(g => g.Title),
                "date_asc" => filteredGames.OrderBy(g => g.ReleaseDate),
                "date_desc" => filteredGames.OrderByDescending(g => g.ReleaseDate),
                "rating_asc" => filteredGames.OrderBy(g => g.Rating),
                "rating_desc" => filteredGames.OrderByDescending(g => g.Rating),
                _ => filteredGames
            };

            return View(filteredGames.ToList());
        }


        public IActionResult Detail(int id)
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }
    }
}
