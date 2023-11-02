using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASPNET_Blog.Models;

namespace ASPNET_Blog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        User user = new User();
        
        // user.Name = "TestName2";
        // user.Email = "TestEmail2";
        // user.Password = "TestPassword2";
        
        // user = user.Find(1);
        // user.Name = "AWOOGA2";
        
        // user.Save();

        Post post = new Post();
        
        // post.Title = "Post Title";
        // post.Body = "Post Body";
        // post.Accessibility = 0;
        // post.CreatedAt = DateTime.Now;
        // post.UserId = 1;

        // post = post.Find(1);
        // post.Title = "Title Update";
        // post.UpdatedAt = DateTime.Now;
        // post.Save();
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
