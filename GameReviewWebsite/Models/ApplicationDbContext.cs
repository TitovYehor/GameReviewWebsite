using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameReviewWebsite.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Title = "Game 1", Genre = "Action", ReleaseDate = new DateTime(2021, 1, 1), Rating = 4.5, Description = "Exciting action game." },
                new Game { Id = 2, Title = "Game 2", Genre = "Adventure", ReleaseDate = new DateTime(2022, 5, 3), Rating = 4.2, Description = "Immersive adventure game." }
            );
        }
    }
}
