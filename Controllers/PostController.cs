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
        UPR.Posts.Reverse();
        return View(UPR);
    }
    public IActionResult Feed()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        ViewData["IsLoggedIn"] = true;
        ViewData["User"] = new User().Find(userId);
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.Posts = ((User)new User().Find(userId)).PostsForMe();
        UPR.Posts = UPR.Posts.OrderBy(x => x.UpdatedAt).ToList();
        UPR.Posts.Reverse();
        return View(UPR);
    }
    public IActionResult Create()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        ViewData["IsLoggedIn"] = true;
        ViewData["User"] = new User().Find(userId);
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
        ViewData["User"] = new User().Find(userId);
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
    public IActionResult DeleteSubmit(int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");
        User loggedUser = new User().Find(userId);
        Post post = new Post().Find(id);
        if (post.UserId != userId)
        {
            if (loggedUser.Role != "admin")
            {
                return RedirectToAction("Index", "Post");
            }
        }
        foreach (var comment in post.Comments())
        {
            comment.Delete();
        }
        foreach (var ratings in post.Ratings())
        {
            ratings.DeleteWhere($"post_id = {post.Id}");
        }
        post.Delete();

        return RedirectToAction("Index", "Post");
    }
    public IActionResult Show(int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        //Code goes here
        UPR.Post = new Post().Find(id);
        if (userId != 0)
        {
            ViewData["IsLoggedIn"] = true;
            ViewData["User"] = new User().Find(userId);
        }
        else
        {
            ViewData["IsLoggedIn"] = false;
            if (UPR.Post.Accessibility != 0)
            {
                return RedirectToAction("Login", "User");
            }
        }
        return View(UPR);
    }
}