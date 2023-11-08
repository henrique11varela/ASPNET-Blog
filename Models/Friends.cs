using ASPNET_Blog.Services;
using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Models
{
    public class Friends : ModelTemplate
    {
        public override string _TableName { get; set; } = "friends";
        public int UserId1 { get; set; } = 0;
        public int UserId2 { get; set; } = 0;

        public override Friends Filler(SqliteDataReader reader)
        {
            return new Friends
            {
                UserId1 = reader.GetInt32(0),
                UserId2 = reader.GetInt32(1)
            };
        }
        public override void Save()
        {
            if (new Friends().Where($"(user1_id = {this.UserId1} AND user2_id = {this.UserId2}) OR (user1_id = {this.UserId2} AND user2_id = {this.UserId1})").Count > 0)
            {
                return;
            }
            string command = $"INSERT INTO {_TableName} (user1_id, user2_id) VALUES ('{this.UserId1}', '{this.UserId2}');";
            AccessDb(command);
        }

        public List<Friends> Where(string conditions)
        {
            List<Friends> list = new List<Friends>();
            List<dynamic> tempList = new Friends().Iwhere(conditions);
            foreach (var item in tempList)
            {
                list.Add(item);
            }
            return list;
        }

        public List<User> Of(int id)
        {
            List<User> list = new List<User>();
            List<Friends> tempList = new Friends().Where($"user1_id = {id} OR user2_id = {id}");
            foreach (Friends item in tempList)
            {
                if (item.UserId1 == id)
                {
                    list.Add(new User().Find(item.UserId2));
                }
                else
                {
                    list.Add(new User().Find(item.UserId1));
                }
            }
            return list;
        }
    }
}
