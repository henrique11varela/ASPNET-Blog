using Microsoft.AspNetCore.Mvc;
using ASPNET_Blog.Models;
using ASPNET_Blog.Models.ViewModels;
using ASPNET_Blog.Services;
using System.Text.RegularExpressions;

namespace ASPNET_Blog.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Index");
        //Code goes here
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.Users = new User().All();
        return View(UPR);
    }

    public IActionResult Edit()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Index");
        //Code goes here
        return View();
    }

    public IActionResult Login()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId != 0) return RedirectToAction("Feed", "Post");
        return View();
    }

    public IActionResult LoginSubmit(User user)
    {
        List<User> list = new User().Where($"username = '{user.Name}' and password = '{user.Password}'");
        if (list.Count > 0)
        {
            AuthLogic.WriteCookie(list[0].Id, Response);
            return RedirectToAction("Feed", "Post");
        }
        return RedirectToAction("Login", "User");
    }

    public IActionResult Register()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId != 0) return RedirectToAction("Feed", "Post");
        return View();
    }

    public IActionResult RegisterSubmit(UserPostRatingViewModel UPR)
    {
        bool isValid = true;
        // Validate Username
        List<User> list = new List<User>();
        if (UPR.User.Name != null)
        {
            list = new User().Where($"username = '{UPR.User.Name}'");
            if (list.Count > 0)
            {
                isValid = false;
            }
        }
        else
        {
            isValid = false;
        }
        // Validate Email
        if (isValid && UPR.User.Email != null)
        {
            Regex reg = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (!reg.IsMatch(UPR.User.Email))
            {
                isValid = false;
            }
        }
        //Validate Password
        if (isValid && UPR.User.Password != UPR.PasswordConfirm)
        {
            isValid = false;
        }
        if (!isValid)
        {
            return RedirectToAction("Register", "User");
        }
        UPR.User.Save();
        AuthLogic.WriteCookie(UPR.User.Id, Response);
        return RedirectToAction("Feed", "Post");
    }

    public IActionResult Show()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Index");
        //Code goes here
        return View();
    }
}