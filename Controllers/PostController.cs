using Microsoft.AspNetCore.Mvc;
using ASPNET_Blog.Models;
using ASPNET_Blog.Models.ViewModels;

namespace ASPNET_Blog.Controllers;

public class PostController : Controller
{
    public IActionResult Index()
    {
        UserPostRatingViewModel posts = new UserPostRatingViewModel();
        posts.Posts = new Post().All();
        return View(posts);
    }
    public IActionResult Create()
    {
        return View();
    }
    public void CreateSubmit(Post post)
    { 
        // missing validations
        post.Save(); 
        Response.Redirect("/post");
    }
    public IActionResult Edit()
    {
        return View();
    }
    public IActionResult Show(Post post, int id)
    {
        post.Find(id:id);
        return View();
    }
}