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
        if (userId == 0) return RedirectToAction("Login");
        ViewData["IsLoggedIn"] = true;
        ViewData["User"] = new User().Find(userId);
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.Users = new User().All();
        return View(UPR);
    }

    public IActionResult Edit()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login");
        ViewData["IsLoggedIn"] = true;
        ViewData["User"] = new User().Find(userId);
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.User = new User().Find(userId);
        return View(UPR);
    }

    public IActionResult EditSubmit(UserPostRatingViewModel UPR)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Edit", "User");

        bool isValid = true;

        List<User> list = new List<User>();
        // Validate Name
        if (UPR.User.Name != null && UPR.User.Name != ((User)new User().Find(userId)).Name)
        {
            list = new User().Where($"username LIKE '{UPR.User.Name}'");
            if (list.Count > 0)
            {
                isValid = false;
            }
        }
        else if (UPR.User.Name == null)
        {
            isValid = false;
        }
        list = new List<User>();

        // Validate Email
        if (UPR.User.Email != null && UPR.User.Email != ((User)new User().Find(userId)).Email)
        {
            var reg = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (!reg.IsMatch(UPR.User.Email))
            {
                isValid = false;
            }
            list = new User().Where($"email LIKE '{UPR.User.Email}'");
            if (list.Count > 0)
            {
                isValid = false;
            }
        }
        else if (UPR.User.Email == null)
        {
            isValid = false;
        }

        // Validate Password
        if (isValid && UPR.User.Password != ((User)new User().Find(userId)).Password)
        {
            if (UPR.User.Password == null || UPR.User.Password != UPR.PasswordConfirm)
            {
                isValid = false;
            }
        }

        if (!isValid) return RedirectToAction("Edit", "User");

        UPR.User.Save();

        return RedirectToAction("Show", "User", new { id = userId });
    }

    public IActionResult Login()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId != 0) return RedirectToAction("Feed", "Post");
        ViewData["IsLoggedIn"] = false;
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        return View(UPR);
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

    public IActionResult LogoutSubmit()
    {
        AuthLogic.ClearCookie(Response);
        return RedirectToAction("Index", "Post");
    }

    public IActionResult Register()
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId != 0) return RedirectToAction("Feed", "Post");
        ViewData["IsLoggedIn"] = false;
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        return View(UPR);
    }

    public IActionResult RegisterSubmit(UserPostRatingViewModel UPR)
    {
        bool isValid = true;
        // Validate Username
        List<User> list = new List<User>();
        if (UPR.User.Name != null)
        {
            // Validate Email
            if (UPR.User.Email != null)
            {
                Regex reg = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                if (!reg.IsMatch(UPR.User.Email))
                {
                    isValid = false;
                }
                list = new User().Where($"username LIKE '{UPR.User.Name}' OR email LIKE '{UPR.User.Email}'");
                if (list.Count > 0)
                {
                    isValid = false;
                }
            }
        }
        else
        {
            isValid = false;
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

    public IActionResult Show(int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        string isFollowing = "";
        if (userId == 0)
        {
            ViewData["IsLoggedIn"] = false;
        }
        else
        {
            ViewData["IsLoggedIn"] = true;
            ViewData["User"] = new User().Find(userId);
            if (userId == id)
            {
                isFollowing = ", 1, 2";
            }
            else
            {
                List<User> following = new User().Find(userId).Following();
                foreach (var item in following)
                {
                    if (item.Id == id)
                    {
                        isFollowing = ", 1";
                    }
                }
            }
        }
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.User = new User().Find(id);
        UPR.Posts = new Post().Where($"user_id = {id} AND accessibility IN (0{(isFollowing)})");
        UPR.Posts.Reverse();
        return View(UPR);
    }

    public IActionResult Followers(int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login");
        ViewData["IsLoggedIn"] = true;
        ViewData["User"] = new User().Find(userId);
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.Users = ((User)new User().Find(id)).Followed();
        return View(UPR);
    }

    public IActionResult Following(int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login");
        ViewData["IsLoggedIn"] = true;
        ViewData["User"] = new User().Find(userId);
        UserPostRatingViewModel UPR = new UserPostRatingViewModel();
        UPR.Users = ((User)new User().Find(id)).Following();
        return View(UPR);
    }

    public IActionResult FollowSubmit(int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Show", "User", new { id = id });
        if (userId != id)
        {
            ((User)new User().Find(userId)).Follow(id);
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }

    public IActionResult UnFollowSubmit(int id)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Show", "User", new { id = id });
        if (userId != id)
        {
            ((User)new User().Find(userId)).UnFollow(id);
        }
        return Redirect(Request.Headers["Referer"].ToString());
    }
}