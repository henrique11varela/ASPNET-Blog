using ASPNET_Blog.Services;
using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Models
{
    public class Comment : ModelTemplate
    {
        public override string _TableName { get; set; } = "comments";
        public int UserId { get; set; } = 0;
        public int PostId { get; set; } = 0;
        public string Body { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public override Comment Filler(SqliteDataReader reader)
        {
            return new Comment
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                PostId = reader.GetInt32(2),
                Body = reader.GetString(3),
                CreatedAt = reader.GetDateTime(4)
            };
        }
        public override void Save()
        {
            if (this.Id == 0)
            {
                string command = $"INSERT INTO {_TableName} (user_id, post_id, body, created_at) VALUES ({this.UserId}, {this.PostId}, '{this.Body}', '{this.CreatedAt}');";
                AccessDb(command);
                command = $"SELECT * FROM {_TableName} ORDER BY id DESC LIMIT 1;";
                this.Id = AccessDb(command)[0].Id;
            }

        }

        public List<Comment> Where(string conditions)
        {
            List<Comment> list = new List<Comment>();
            List<dynamic> tempList = new Comment().Iwhere(conditions);
            foreach (var item in tempList)
            {
                list.Add(item);
            }
            return list;
        }

        public User User()
        {
            return new User().Find(this.UserId);
        }
    }
}
