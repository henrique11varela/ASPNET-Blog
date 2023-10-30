using Microsoft.AspNetCore.Mvc;
using ASPNET_Blog.Models;

namespace ASPNET_Blog.Controllers;

public class RatingController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}