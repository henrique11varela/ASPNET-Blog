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
        public string Role { get; set; } = "";
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
                Role = reader.GetString(4),
                CreatedAt = reader.GetDateTime(5),
                UpdatedAt = reader.GetDateTime(6)
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
                command += $"role = '{this.Role}', ";
                command += $"updated_at = '{DateTime.Now}' ";
                command += $"WHERE id = {this.Id}";
            }
            else
            {
                command += $"INSERT INTO {_TableName} (username, email, password, role, created_at, updated_at) VALUES ('{this.Name}','{this.Email}','{this.Password}','member', '{DateTime.Now}', '{DateTime.Now}');";
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

        public List<User> Following()
        {
            return new Follows().Following(this.Id);
        }

        public List<User> Followed()
        {
            return new Follows().Followed(this.Id);
        }
        
        public bool isFollowing(int id)
        {
            List<Follows> follows = new Follows().Where($"user_id = {this.Id} AND following_id = {id}");
            if (follows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void Follow(int id)
        {
            Follows follow = new Follows();
            follow.UserId = this.Id;
            follow.FollowingId = id;
            follow.Save();
        }

        public void UnFollow(int id)
        {
            List<Follows> follows = new Follows().Where($"user_id = {this.Id} AND following_id = {id}");
            if (follows.Count > 0)
            {
                follows[0].DeleteWhere($"user_id = {this.Id} AND following_id = {id}");
            }
        }

        public List<Post> PostsForMe()
        {
            string friends = "";
            List<User> f = new User().Find(this.Id).Following();
            foreach (var item in f)
            {
                friends += ", " + item.Id;
            }
            List<Post> list = new Post().Where($"(accessibility = 1 AND user_id IN ({this.Id + friends})) OR accessibility = 0");
            return list;
        }
    }
}
