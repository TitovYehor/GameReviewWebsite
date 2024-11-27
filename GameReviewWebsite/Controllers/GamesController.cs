using Microsoft.AspNetCore.Mvc;
using GameReviewWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace GameReviewWebsite.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        { 
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Games.Add(game);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(game);
        }

        public IActionResult List(string searchString, string genre, double? minRating, string sortOrder)
        {
            var filteredGames = _context.Games.AsQueryable();

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

            //var gameReviews = _context.Reviews.Where(r => r.GameId == id).ToList();
            //var gameReviews = _context.Reviews
            //    .Where(r => r.GameId == id)
            //    .Select(r => new
            //    {
            //        r.Title,
            //        r.Content,
            //        r.Rating,
            //        r.UserId,
            //        r.UserNickname = _context.Users.Where(u => u.Id == r.UserId).Select(u => u.Nickname).FirstOrDefault()
            //    })
            //    .ToList();
            var gameReviews = _context.Reviews
                .Where(r => r.GameId == id)
                .Select(r => new Review
                {
                    Id = r.Id,
                    Title = r.Title,
                    Content = r.Content,
                    Rating = r.Rating,
                    UserId = r.UserId,
                    GameId = r.GameId,
                    UserNickname = _context.Users
                        .Where(u => u.Id == r.UserId)
                        .Select(u => u.Nickname) 
                        .FirstOrDefault()
                })
                .ToList();

            ViewBag.Reviews = gameReviews;

            return View(game);
        }
    }

    public class GameDetailViewModel
    {
        public Game Game { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
