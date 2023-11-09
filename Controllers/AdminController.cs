using Microsoft.AspNetCore.Mvc;
using ASPNET_Blog.Models;
using ASPNET_Blog.Models.ViewModels;
using ASPNET_Blog.Services;

namespace ASPNET_Blog.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        User user = new User().Find(userId);
        if (user.Role != "admin") return RedirectToAction("Index", "Post");
        ViewData["IsLoggedIn"] = true;
        ViewData["User"] = user;

        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.Users = new User().All();

        return View(UPR);
    }

    public IActionResult AddAdmin(int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        User user = new User().Find(userId);
        if (user.Role != "admin") return RedirectToAction("Index", "Post");
        ViewData["IsLoggedIn"] = true;
        ViewData["User"] = user;

        User targetUser = new User().Find(id);
        targetUser.Role = "admin";
        targetUser.Save();

        return RedirectToAction("Index", "Admin");
    }
    
    public IActionResult RevokeAdmin(int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        User user = new User().Find(userId);
        if (user.Role != "admin") return RedirectToAction("Index", "Post");
        ViewData["IsLoggedIn"] = true;
        ViewData["User"] = user;

        User targetUser = new User().Find(id);
        targetUser.Role = "member";
        targetUser.Save();

        return RedirectToAction("Index", "Admin");
    }
}