using ASPNET_Blog.Services;
using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Models
{
    public class User : ModelTemplate
    {
        public override string _TableName { get; set; } = "users";
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public override User Filler(SqliteDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Password = reader.GetString(2),
                Email = reader.GetString(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = reader.GetDateTime(5)
            };
        }
        public override void Save()
        {
            string command = "";
            if (this.Id > 0)
            {
                command += $"UPDATE {_TableName} SET ";
                command += $"username = '{this.Name}', ";
                command += $"email = '{this.Email}', ";
                command += $"password = '{this.Password}', ";
                command += $"created_at = '{this.CreatedAt}', ";
                command += $"updated_at = '{DateTime.Now}' ";
                command += $"WHERE id = {this.Id}";
            }
            else
            {
                command += $"INSERT INTO {_TableName} (username, email, password, created_at, updated_at) VALUES ('{this.Name}','{this.Email}','{this.Password}', '{DateTime.Now}', '{DateTime.Now}');";
            }
            AccessDb(command);
            if (command[0].ToString().ToLower() == "i")
            {
                command = $"SELECT * FROM {_TableName} ORDER BY id DESC LIMIT 1;";
                this.Id = AccessDb(command)[0].Id;
                AuthLogic.AddNewCookie(this.Id);
            }
        }

        public List<User> All()
        {
            List<User> list = new List<User>();
            List<dynamic> tempList = new User().Iall();
            foreach (var item in tempList)
            {
                list.Add(item);
            }
            return list;
        }
        public List<User> Where(string conditions)
        {
            List<User> list = new List<User>();
            List<dynamic> tempList = new User().Iwhere(conditions);
            foreach (var item in tempList)
            {
                list.Add(item);
            }
            return list;
        }
        public List<Post> Posts()
        {
            return new Post().Where($"user_id = {this.Id}");
        }

        public List<User> Friends()
        {
            return new Friends().Of(this.Id);
        }

        public List<Post> PostsForMe()
        {
            List<Post> list = new List<Post>();
            string friends = "";
            List<User> f = new Friends().Of(this.Id);
            foreach (var item in f)
            {
                friends += ", " + item.Id;
            }
            List<dynamic> tempList = AccessDb($"SELECT * FROM {new Post()._TableName} WHERE accessibility IN (0, 1) AND user_id IN ({this.Id}, {friends})");
            foreach (var item in tempList)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
