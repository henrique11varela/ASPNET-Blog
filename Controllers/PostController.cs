using Microsoft.AspNetCore.Mvc;
using ASPNET_Blog.Models;

namespace ASPNET_Blog.Controllers;

public class PostController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Create()
    {
        return View();
    }
    public IActionResult Edit()
    {
        return View();
    }
    public IActionResult Show()
    {
        return View();
    }
}