using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Models
{
    public class Rating : ModelTemplate
    {
        public override string _TableName { get; set; } = "ratings";

        public int UserId { get; set; } = 0;
        public int PostId { get; set; } = 0;
        public int Value { get; set; } = 0;
        

        public override Rating Filler(SqliteDataReader reader)
        {
            return new Rating
            {
                UserId = reader.GetInt32(0),
                PostId = reader.GetInt32(1),
                Value = reader.GetInt32(2)
            };
        }
        public override void Save()
        {
            List<Rating> curr = new Rating().Where($"user_id = {this.UserId} AND post_id = {this.PostId}");
            string command = "";
            if (curr.Count > 0)
            {
                command += $"UPDATE {_TableName} SET ";
                command += $"rating = {this.Value} ";
                command += $"WHERE user_id = {this.UserId} AND post_id = {this.PostId}";
            }
            else
            {
                command += $"INSERT INTO {_TableName} (user_id, post_id, rating) VALUES ({this.UserId}, {this.PostId}, {this.Value});";
            }
            AccessDb(command);
        }

        public List<Rating> All()
        {
            List<Rating> list = new List<Rating>();
            List<dynamic> tempList = new Rating().Iall();
            foreach (var item in tempList)
            {
                list.Add(item);
            }
            return list;
        }

        public List<Rating> Where(string conditions){
            List<Rating> list = new List<Rating>();
            List<dynamic> tempList = new Rating().Iwhere(conditions);
            foreach (var item in tempList)
            {
                list.Add(item);
            }
            return list;
        }

        public override void Delete()
        {
            this.DeleteWhere($"user_id = {this.UserId} AND post_id = {this.PostId}");
        }
    }
}