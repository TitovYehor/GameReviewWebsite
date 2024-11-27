using Microsoft.AspNetCore.Mvc;
using GameReviewWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GameReviewWebsite.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(int gameId, string title, string content, int rating)
        {
            var username = User?.Identity?.Name; 

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized(); 
            }

            var userId = _context.Users
                .Where(u => u.UserName == username)
                .Select(u => u.Id)
                .FirstOrDefault();

            var newReview = new Review
            {
                Title = title,
                Content = content,
                Rating = rating,
                UserId = userId,
                GameId = gameId,
                UserNickname = _context.Users
                    .Where(u => u.UserName == username)
                    .Select(u => u.Nickname)
                    .FirstOrDefault()
            };

            _context.Reviews.Add(newReview);
            await _context.SaveChangesAsync();

            await UpdateGameRatingAsync(gameId);

            return RedirectToAction("Detail", "Games", new { id = gameId });
        }

        private async Task UpdateGameRatingAsync(int gameId)
        {
            var game = await _context.Games
                .Include(g => g.Reviews)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (game != null && game.Reviews.Any())
            {
                game.Rating = game.Reviews.Average(r => r.Rating);
                await _context.SaveChangesAsync();
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReview(int reviewId, string title, string content, int rating)
        {
            var review = await _context.Reviews.FindAsync(reviewId);

            if (review == null)
            {
                return NotFound();
            }

            review.Title = title;
            review.Content = content;
            review.Rating = rating;

            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();

            await UpdateGameRatingAsync(review.GameId);

            return RedirectToAction("Detail", "Games", new { id = review.GameId });
        }
    }
}