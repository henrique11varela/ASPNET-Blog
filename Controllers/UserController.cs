using Microsoft.AspNetCore.Mvc;
using ASPNET_Blog.Models;
using ASPNET_Blog.Models.ViewModels;

namespace ASPNET_Blog.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        UserPostRatingViewModel bag = new UserPostRatingViewModel();

        bag.Users = new User().All();
        // bag.User = new User().Find(1);

        return View(bag);
    }
    public IActionResult Edit()
    {
        return View();
    }
    public IActionResult Login()
    {
        //check if logged
        //  redirect feed
        return View();
    }
    public void LoginSubmit(User user)
    {
        //check if user valid
        //  save cookie
        //  redirect feed
        //redirect Login
    }
    public IActionResult Register()
    {
        return View();
    }
    public void RegisterSubmit(User user)
    {
        //Validate
        user.Save();
        Response.Redirect("/user");
    }
    public IActionResult Show()
    {
        return View();
    }
}