using Microsoft.AspNetCore.Identity;

namespace GameReviewWebsite.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nickname { get; set; }
    }
}
