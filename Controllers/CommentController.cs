using Microsoft.AspNetCore.Mvc;
using ASPNET_Blog.Models;
using ASPNET_Blog.Models.ViewModels;
using ASPNET_Blog.Services;

namespace ASPNET_Blog.Controllers;

public class CommentController : Controller
{
    public IActionResult AddComment(UserPostRatingViewModel UPR){
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");

        if (UPR.Comment.Body.Length > 0)
        {
            UPR.Comment.UserId = userId;
            UPR.Comment.Save();
        }
        
        return Redirect(Request.Headers["Referer"].ToString());
    }
}