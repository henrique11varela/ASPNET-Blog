using Microsoft.AspNetCore.Mvc;
using ASPNET_Blog.Models;
using ASPNET_Blog.Services;

namespace ASPNET_Blog.Controllers;

public class RatingController : Controller
{
    public IActionResult Rate(int id, int rate)
    {
        int userId = AuthLogic.ValidateUser(Request);
        if (userId == 0) return RedirectToAction("Login", "User");

        List<Rating> ratings = new Rating().Where($"user_id = {userId} AND post_id = {id}");
        Rating tempRating = new Rating();
        //check if already rated
        if (rate > 0 && rate < 6)
        {
            if (ratings.Count > 0)
            {
                ratings[0].Value = rate;
                ratings[0].Save();
            }
            else
            {
                tempRating.UserId = userId;
                tempRating.PostId = id;
                tempRating.Value = rate;
                tempRating.Save();
            }
        }
        return Redirect(Request.Headers["Referer"].ToString());
    }
}