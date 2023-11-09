using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Services;
// Guid uuid = Guid.NewGuid();
// Response.Cookies.Append("TestCookie", uuid.ToString());
// string test = Request.Cookies["TestCookie"].ToString();
public class AuthLogic
{
    /*
    *   Validates if the cookie "UUID" is valid
    *   and returns the id of the user.
    *   If it's not valid, redirects to the login page
    */
    public static int ValidateUser(HttpRequest Request)
    {
        string? cookie = Request.Cookies["UUID"]?.ToString();
        if (cookie != null)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            var config = builder.AddJsonFile(@"appsettings.json").Build();
            using (SqliteConnection connection = new SqliteConnection(config.GetConnectionString("ASPNETBlogContext")))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "SELECT * FROM usercookie";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (cookie == reader.GetString(1))
                                {
                                    return reader.GetInt32(0);
                                }
                            }
                        }
                    }
                    connection.Close();
                }
            }
        }
        return 0;
    }

    public static void AddNewCookie(int id)
    {
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        var config = builder.AddJsonFile(@"appsettings.json").Build();
        using (SqliteConnection connection = new SqliteConnection(config.GetConnectionString("ASPNETBlogContext")))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = $"INSERT INTO usercookie (user_id, cookie) VALUES ({id},'{Guid.NewGuid()}');";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    throw new Exception("Query could not run!: " + ex);
                }
                connection.Close();
            }
        }
    }

    public static void WriteCookie(int id, HttpResponse Response)
    {
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        var config = builder.AddJsonFile(@"appsettings.json").Build();
        using (SqliteConnection connection = new SqliteConnection(config.GetConnectionString("ASPNETBlogContext")))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = $"SELECT * FROM usercookie WHERE user_id = {id}";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CookieOptions co = new CookieOptions();
                            co.HttpOnly = true;
                            co.Expires = DateTime.Now.AddHours(2);
                            Response.Cookies.Append("UUID", reader.GetString(1), co);
                        }
                    }
                }
                connection.Close();
            }
        }
    }
    public static void ClearCookie(HttpResponse Response)
    {
        Response.Cookies.Append("UUID", "");
    }
}