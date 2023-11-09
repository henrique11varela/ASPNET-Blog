namespace ASPNET_Blog.Models.ViewModels;

public class UserPostRatingViewModel
{
    public string PasswordConfirm {get;set;}
    public User User {get;set;}
    public List<User> Users {get;set;}
    public Post Post {get;set;}
    public List<Post> Posts {get;set;}
    public Comment Comment {get;set;}
    // public bool IsLoggedIn {get;set;} = true;
}