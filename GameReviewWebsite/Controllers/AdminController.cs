using GameReviewWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users;
        return View(users);
    }

    public async Task<IActionResult> ManageRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var userRoles = await _userManager.GetRolesAsync(user);
        var roles = new[] { "Admin", "Moderator", "User" };

        var model = new ManageRolesViewModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            AssignedRoles = userRoles,
            AllRoles = roles
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRoles(string userId, List<string> roles)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var userRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, userRoles);

        if (roles != null && roles.Any())
        {
            await _userManager.AddToRolesAsync(user, roles);
        }

        return RedirectToAction("Index");
    }
}