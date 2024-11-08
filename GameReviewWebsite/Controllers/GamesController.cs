using Microsoft.AspNetCore.Mvc;

namespace GameReviewWebsite.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            return View();
        }
    }
}
