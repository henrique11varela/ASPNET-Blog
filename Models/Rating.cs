using Microsoft.Data.Sqlite;

namespace ASPNET_Blog.Models
{
    public class Rating : ModelTemplate
    {
        public override string _TableName { get; set; } = "ratings";

        public int One { get; private set; } = 0;
        public int Two { get; private set; } = 0;
        public int Three { get; private set; } = 0;
        public int Four { get; private set; } = 0;
        public int Five { get; private set; } = 0;

        public override Rating Filler(SqliteDataReader reader)
        {
            return new Rating
            {
                Id = reader.GetInt32(0),
                One = reader.GetInt32(1),
                Two = reader.GetInt32(2),
                Three = reader.GetInt32(3),
                Four = reader.GetInt32(4),
                Five = reader.GetInt32(5)
            };
        }
        public double AddRating(int stars){
            switch (stars)
            {
                case 1:
                    One++;
                    break;
                case 2:
                    Two++;
                    break;
                case 3:
                    Three++;
                    break;
                case 4:
                    Four++;
                    break;
                case 5:
                    Five++;
                    break;
                default:
                    return 0;
            }
            return (One + Two * 2 + Three * 3 + Four * 4 + Five * 5) / (One + Two + Three + Four + Five);
        }
        public double ReadRating(){
            return (One + Two * 2 + Three * 3 + Four * 4 + Five * 5) / (One + Two + Three + Four + Five);
        }
        public override void Save()
        {
            string command = "";
            if (this.Id > 0)
            {
                command += $"UPDATE {_TableName} SET ";
                command += $"id = {this.Id}, ";
                command += $"one = {this.One}, ";
                command += $"two = {this.Two}, ";
                command += $"three = {this.Three}, ";
                command += $"four = {this.Four}, ";
                command += $"five = {this.Five}, ";
                command += $"WHERE id = {this.Id}";
            }
            else
            {
                command += $"INSERT INTO {_TableName} (id, one, two, three, four, five) VALUES ({this.Id}, {this.One}, {this.Two}, {this.Three}, {this.Four}, {this.Five});";
            }
            AccessDb(command);
            if (command[0].ToString().ToLower() == "i")
            {
                command = $"SELECT * FROM {_TableName} ORDER BY id DESC LIMIT 1;";
                this.Id = AccessDb(command)[0].Id;
            }
        }
    }
}