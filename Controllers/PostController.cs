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
        ViewData["IsLoggedIn"] = false;
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.Posts = new Post().Where("accessibility = 0");
        return View(UPR);
    }
    public IActionResult Feed()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        ViewData["IsLoggedIn"] = true;
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.Posts = ((User)new User().Find(userId)).PostsForMe();
        return View(UPR);
    }
    public IActionResult Create()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        ViewData["IsLoggedIn"] = true;
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        //Code goes here
        return View(UPR);
    }
    public IActionResult CreateSubmit(UserPostRatingViewModel UPR)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        
        if (UPR.Post.Title == null || UPR.Post.Body == null || UPR.Post.Accessibility == null)
        {
            return RedirectToAction("Create", "Post");
        }
        
        UPR.Post.UserId = userId;
        UPR.Post.Save();
        
        return RedirectToAction("Show", "Post", new { id = UPR.Post.Id });
    }
    public IActionResult Edit(int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        ViewData["IsLoggedIn"] = true;
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        //Code goes here
        UPR.Post = new Post().Find(id);
        return View(UPR);
    }
    public IActionResult EditSubmit(UserPostRatingViewModel UPR)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        
        if (UPR.Post.Title == null || UPR.Post.Body == null || UPR.Post.Accessibility == null)
        {
            return RedirectToAction("Edit", "Post", new { id = UPR.Post.Id });
        }
        
        UPR.Post.UserId = userId;
        UPR.Post.Save();
        
        return RedirectToAction("Show", "Post", new { id = UPR.Post.Id });
    }
    public IActionResult Show(int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        ViewData["IsLoggedIn"] = true;
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        //Code goes here
        UPR.Post = new Post().Find(id);
        return View(UPR);
    }
}