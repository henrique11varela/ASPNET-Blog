using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

string databaseFilePath = "./data/ASPNET_Blog.sqlite";
if (!File.Exists(databaseFilePath))
{
    File.Create(databaseFilePath).Close();
    using (SqliteConnection connection = new SqliteConnection(app.Configuration.GetConnectionString("ASPNETBlogContext")))
    {
        using (var command = connection.CreateCommand())
        {
            connection.Open();
            command.CommandText = "CREATE TABLE users ( id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT, password TEXT, email TEXT, created_at TEXT, updated_at TEXT);CREATE TABLE posts ( id INTEGER PRIMARY KEY AUTOINCREMENT, title TEXT, body TEXT, accessibility INTEGER, created_at TEXT, updated_at TEXT, user_id INTEGER, FOREIGN KEY (user_id) REFERENCES users (id));CREATE TABLE ratings ( id INTEGER PRIMARY KEY AUTOINCREMENT, one INTEGER, two INTEGER, three INTEGER, four INTEGER, five INTEGER, post_id INTEGER, FOREIGN KEY (post_id) REFERENCES posts (id));CREATE TABLE friends ( user1_id INTEGER, user2_id INTEGER, PRIMARY KEY (user1_id, user2_id), FOREIGN KEY (user1_id) REFERENCES users (id), FOREIGN KEY (user2_id) REFERENCES users (id));CREATE TABLE usercookie (user_id INTEGER, cookie text, PRIMARY KEY (user_id), FOREIGN KEY (user_id) REFERENCES users (id));";
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception)
            {
                throw new Exception("Query could not run!");
            }
            connection.Close();
        }
    }
}

app.Run();
