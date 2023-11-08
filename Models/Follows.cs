using ASPNET_Blog.Services;
using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Models
{
    public class Follows : ModelTemplate
    {
        public override string _TableName { get; set; } = "follows";
        public int UserId { get; set; } = 0;
        public int FollowingId { get; set; } = 0;

        public override Follows Filler(SqliteDataReader reader)
        {
            return new Follows
            {
                UserId = reader.GetInt32(0),
                FollowingId = reader.GetInt32(1)
            };
        }
        public override void Save()
        {
            if (new Follows().Where($"user_id = {this.UserId} AND following_id = {this.FollowingId}").Count > 0)
            {
                return;
            }
            string command = $"INSERT INTO {_TableName} (user_id, following_id) VALUES ('{this.UserId}', '{this.FollowingId}');";
            AccessDb(command);
        }

        public List<Follows> Where(string conditions)
        {
            List<Follows> list = new List<Follows>();
            List<dynamic> tempList = new Follows().Iwhere(conditions);
            foreach (var item in tempList)
            {
                list.Add(item);
            }
            return list;
        }

        public List<User> Following(int id)
        {
            List<User> list = new List<User>();
            List<Follows> tempList = new Follows().Where($"user_id = {id}");
            foreach (Follows item in tempList)
            {
                    list.Add(new User().Find(item.FollowingId));
            }
            return list;
        }
    }
}
