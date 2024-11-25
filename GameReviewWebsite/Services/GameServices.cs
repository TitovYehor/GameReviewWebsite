using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameReviewWebsite.Models;

namespace GameReviewWebsite.Services
{
    public class GameService
    {
        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpdateGameRatingAsync(int gameId)
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
    }
}
