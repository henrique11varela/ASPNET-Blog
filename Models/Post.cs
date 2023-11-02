using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Models
{
    public class Post : ModelTemplate
    {
        public override string _TableName {get;set;} = "posts";
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public int Accessibility { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; } = 1;
    
    
        public override Post Filler(SqliteDataReader reader)
        {
            return new Post
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Body = reader.GetString(2),
                Accessibility = reader.GetInt32(3),
                CreatedAt = reader.GetDateTime(4),
                UpdatedAt = reader.GetDateTime(5),
                UserId = reader.GetInt32(6)
            };
        }

        public override void Save()
        {
            string command = "";
            if (this.Id > 0)
            {
                command += $"UPDATE {_TableName} SET ";
                command += $"title = '{this.Title}', ";
                command += $"body = '{this.Body}', ";
                command += $"accessibility = '{this.Accessibility}', ";
                command += $"created_at = '{this.CreatedAt}', ";
                command += $"updated_at = '{this.UpdatedAt}', ";
                command += $"user_id = '{this.UserId}' ";
                command += $"WHERE id = {this.Id}";
            }
            else
            {
                command += $"INSERT INTO {_TableName} (title, body, accessibility, created_at, updated_at, user_id) VALUES ('{this.Title}','{this.Body}','{this.Accessibility}','{this.CreatedAt}','{this.UpdatedAt}','{this.UserId}');";
            }
            AccessDb(command);
        }
    }
}