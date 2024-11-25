using System.Collections.Generic;

namespace GameReviewWebsite.Models
{
    public class ManageRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> AssignedRoles { get; set; }
        public string[] AllRoles { get; set; }
    }
}