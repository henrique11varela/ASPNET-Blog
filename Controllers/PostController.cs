using Microsoft.AspNetCore.Mvc;
using ASPNET_Blog.Models;
using ASPNET_Blog.Models.ViewModels;
using ASPNET_Blog.Services;

namespace ASPNET_Blog.Controllers;

public class PostController : Controller
{
    public IActionResult Index()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId != 0) return RedirectToAction("Feed");
        //Code goes here
        return View();
    }
    public IActionResult Feed()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        //Code goes here
        return View();
    }
    public IActionResult Create()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        //Code goes here
        return View();
    }
    public IActionResult Edit()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        //Code goes here
        return View();
    }
    public IActionResult Show()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        //Code goes here
        return View();
    }
}