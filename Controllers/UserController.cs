using Microsoft.AspNetCore.Mvc;
using ASPNET_Blog.Models;

namespace ASPNET_Blog.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}