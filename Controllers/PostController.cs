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
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.IsLoggedIn = false;
        UPR.Posts = new Post().All();
        return View(UPR);
    }
    public IActionResult Feed()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.Posts = new Post().All();
        return View(UPR);
    }
    public IActionResult Create()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        //Code goes here
        return View(UPR);
    }
    public void CreateSubmit(Post post)
    {
        // missing validations
        post.Save();
        Response.Redirect("/post");
    }
    public IActionResult Edit()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        //Code goes here
        return View(UPR);
    }
    public IActionResult Show(Post post, int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        //Code goes here
        return View(UPR);
    }
}