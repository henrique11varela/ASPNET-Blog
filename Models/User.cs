using System.Runtime.InteropServices.JavaScript;
using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Models
{
    public class User : ModelTemplate
    {
        public override string _TableName {get;set;} = "users";
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public override User filler(SqliteDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Password = reader.GetString(2),
                Email = reader.GetString(3)
            };
        }

        public override void save()
        {
            string command = "";
            if (this.Id > 0)
            {
                command += $"UPDATE {_TableName} SET ";
                command += $"username = '{this.Name}', ";
                command += $"email = '{this.Email}', ";
                command += $"password = '{this.Password},' ";
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
