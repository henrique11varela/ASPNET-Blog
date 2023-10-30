namespace ASPNET_Blog.Models;

public class Post
{
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public int Accessibility { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int AverageRating { get; set; } = 0;
}