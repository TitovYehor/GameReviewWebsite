using Microsoft.AspNetCore.Mvc;
using GameReviewWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameReviewWebsite.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        { 
            _context = context;
        }

        //private List<Game> games = new List<Game>
        //{
        //    new Game { Id = 1, Title = "Game 1", Genre = "Action", ReleaseDate = new DateTime(2021, 1, 1), Rating = 4.5, Description = "Exciting action game." },
        //    new Game { Id = 2, Title = "Game 2", Genre = "Adventure", ReleaseDate = new DateTime(2022, 5, 3), Rating = 4.2, Description = "Immersive adventure game." },
        //    new Game { Id = 3, Title = "Game 3", Genre = "Puzzle", ReleaseDate = new DateTime(2020, 3, 15), Rating = 4.8, Description = "Challenging puzzle game." }
        //};

        //private List<Review> reviews = new List<Review>
        //{
        //    new Review { Id = 1, GameId = 1, Rating = 5, Comment = "Amazing game!", ReviewerName = "Alice" },
        //    new Review { Id = 2, GameId = 1, Rating = 4, Comment = "Very fun.", ReviewerName = "Bob" },
        //    new Review { Id = 3, GameId = 2, Rating = 3.5, Comment = "Good but could be better.", ReviewerName = "Charlie" }
        //};

        public IActionResult List(string searchString, string genre, double? minRating, string sortOrder)
        {
            var filteredGames = _context.Games.AsQueryable();

            // Apply filters if any
            if (!string.IsNullOrEmpty(searchString))
            {
                filteredGames = filteredGames.Where(g => g.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                filteredGames = filteredGames.Where(g => g.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
            }

            if (minRating.HasValue)
            {
                filteredGames = filteredGames.Where(g => g.Rating >= minRating.Value);
            }

            // Sorting
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
            var game = _context.Games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            var gameReviews = _context.Reviews.Where(r => r.GameId == id).ToList();
            ViewBag.Reviews = gameReviews;

            return View(game);
        }

        [HttpPost]
        public IActionResult AddReview(int gameId, string reviewerName, double rating, string comment)
        {
            var newReview = new Review
            {
                GameId = gameId,
                ReviewerName = reviewerName,
                Rating = rating,
                Comment = comment
            };

            _context.Reviews.Add(newReview);
            _context.SaveChanges();

            return RedirectToAction("Detail", new { id = gameId });
        }
    }
}
