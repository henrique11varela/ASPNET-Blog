using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Models
{
    public class Post : ModelTemplate
    {
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public int Accessibility { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int AverageRating { get; set; } = 0;
    
    
        public override Post filler(SqliteDataReader reader)
        {
            return new Post
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Body = reader.GetString(2),
                Accessibility = reader.GetInt32(3),
                // CreatedAt = reader.GetDateTime(4),
                // UpdatedAt = reader.GetDateTime(5),
                // AverageRating = reader.GetInt32(6)
            };
        }

        public override void save()
        {
            string command = "";
            if (this.Id > 0)
            {
                command += $"UPDATE {_TableName} SET ";
                command += $"title = '{this.Title}', ";
                command += $"body = '{this.Body}', ";
                command += $"accessibility = '{this.Accessibility}', ";
                // command += $"created_at = '{this.CreatedAt}', ";
                // command += $"updated_at = '{this.UpdatedAt}', ";
                // command += $"average_rating = '{this.AverageRating}' ";
                command += $"WHERE id = {this.Id}";
            }
            else
            {
                command +=
                    $"INSERT INTO {_TableName} (title, body, accessibility, created_at, updated_at, average_rating) VALUES ('{this.Title}','{this.Body}','{this.Accessibility}','{this.CreatedAt}','{this.UpdatedAt}','{this.AverageRating}');";
            }

            AccessDB(command);
        }
    }
}