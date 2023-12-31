using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Models
{
    public class Post : ModelTemplate
    {
        public override string _TableName { get; set; } = "posts";
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public int? Accessibility { get; set; } = null;
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
                command += $"updated_at = '{DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt")}', ";
                command += $"user_id = '{this.UserId}' ";
                command += $"WHERE id = {this.Id}";
            }
            else
            {
                command += $"INSERT INTO {_TableName} (title, body, accessibility, created_at, updated_at, user_id) VALUES ('{this.Title}','{this.Body}','{this.Accessibility}','{DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt")}','{DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt")}','{this.UserId}');";
            }
            AccessDb(command);
            if (command[0].ToString().ToLower() == "i")
            {
                command = $"SELECT * FROM {_TableName} ORDER BY id DESC LIMIT 1;";
                this.Id = AccessDb(command)[0].Id;
            }
        }
        public List<Post> All()
        {
            List<Post> list = new List<Post>();
            List<dynamic> tempList = new Post().Iall();
            foreach (var item in tempList)
            {
                list.Add(item);
            }
            return list;
        }
        public List<Post> Where(string conditions)
        {
            List<Post> list = new List<Post>();
            List<dynamic> tempList = new Post().Iwhere(conditions);
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
        public double Rating()
        {
            List<Rating> ratings = new Rating().Where($"post_id = {this.Id}");
            double output = 0;
            foreach (Rating rating in ratings)
            {
                output += rating.Value;
            }
            if (ratings.Count > 0)
            {
                output = output / ratings.Count;
            }
            return output;
        }
        public List<Rating> Ratings()
        {
            return new Rating().Where($"post_id = {this.Id}");
        }

        public List<Comment> Comments()
        {
            return new Comment().Where($"post_id = {this.Id}");
        }

        public override void Delete()
        {
            foreach (var comment in this.Comments())
            {
                comment.Delete();
            }
            foreach (var rating in this.Ratings())
            {
                rating.Delete();
            }
            this.DeleteWhere($"id = {this.Id}");
        }
    
        public List<Post> FromYear(int year, List<Post> list){
            List<Post> tempList = new List<Post>();
            foreach (var item in list)
            {
                if (item.UpdatedAt.Year == year)
                {
                    tempList.Add(item);
                }
            }
            return tempList;
        }
        
        public List<Post> FromMonth(int month, List<Post> list){
            List<Post> tempList = new List<Post>();
            foreach (var item in list)
            {
                if (item.UpdatedAt.Month == month)
                {
                    tempList.Add(item);
                }
            }
            return tempList;
        }
    
        public List<int> YearsFromList(List<Post> list){
            List<int> tempList = new List<int>();
            foreach (var item in list)
            {
                if (!tempList.Contains(item.UpdatedAt.Year))
                {
                    tempList.Add(item.UpdatedAt.Year);
                }
            }
            return tempList;
        }
        public List<int> MonthsFromList(List<Post> list){
            List<int> tempList = new List<int>();
            foreach (var item in list)
            {
                if (!tempList.Contains(item.UpdatedAt.Month))
                {
                    tempList.Add(item.UpdatedAt.Month);
                }
            }
            return tempList;
        }
    }
}