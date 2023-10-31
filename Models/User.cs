using System.Runtime.InteropServices.JavaScript;
using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Models
{
    public class User : ModelTemplate
    {
        private string _TableName = "users";
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        static User filler(SqliteDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Password = reader.GetString(3)
            };
        }

        public void save()
        {
            string command = "";
            if (this.Id > 0)
            {
                command += $"UPDATE {_TableName} SET ";
                command += this.Name != "" ? $"username = '{this.Name}'" : "";
                command += this.Email != "" ? $"email = '{this.Email}'" : "";
                command += this.Password != "" ? $"password = '{this.Password}'" : "";
                command += $"WHERE id = {this.Id}";
            }
            else
            {
                command += $"INSERT INTO {_TableName} (username, email, password) VALUES ('{this.Name}','{this.Email}','{this.Password}');";
            }
            AccessDB(command);
        }
    }

}