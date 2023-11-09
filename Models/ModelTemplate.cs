using Microsoft.Data.Sqlite;

public interface IModelTemplate { }

/** How to implement
*
* - Fill private string _TableName with the name of the DB table
* - Implement a "filler" method that uses a reader to fill the props
* - Do not implement an "Id" since it is already implemented
*/
public class ModelTemplate : IModelTemplate
{
    public virtual string _TableName { get; set; } = "";
    public int Id { get; set; }

    public virtual IModelTemplate Filler(SqliteDataReader reader)
    {
        return new ModelTemplate();
    }

    /** Stores data into the DataBase
    * 
    */
    public virtual void Save()
    {
        string command = "";
        if (this.Id > 0)
        {
            //command += $"UPDATE {_TableName} SET ";
            //command += this.Name != "" ? $"name = '{this.Name}'" : "";
            //command += $"WHERE id = {this.Id}";
        }
        else
        {
            //command += $"INSERT INTO {_TableName} (name, email, password) VALUES ({this.Name},{this.Email},{this.Password});";
        }
        AccessDb(command);
    }

    /** Returns the item that has the Id "id"
    * 
    */
    public dynamic Find(int id)
    {
        string query = $"SELECT * FROM {this._TableName} WHERE id = {id}";
        return AccessDb(query)[0];
    }

    /** Returns a List<> of items that pass the "conditions"
    * 
    */
    protected List<dynamic> Iall()
    {
        string query = $"SELECT * FROM {_TableName}";
        return AccessDb(query);
    }

    /** Returns a List<> of items that pass the "conditions"
    * 
    */
    protected List<dynamic> Iwhere(string conditions)
    {
        string query = $"SELECT * FROM {_TableName} WHERE {conditions}";
        return AccessDb(query);
    }

    /** Deletes the item that has the Id "id"
    * 
    */
    public void Delete(int id)
    {
        string query = $"DELETE FROM {_TableName} WHERE id = {id}";
        AccessDb(query);
    }

    public void DeleteWhere(string conditions)
    {
        string query = $"DELETE FROM {_TableName} WHERE {conditions}";
        AccessDb(query);
    }

    /** Manages the connection to the DataBase
    * 
    */
    public List<dynamic> AccessDb(string commandString)
    {
        List<dynamic> itemList = new List<dynamic>();
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        var config = builder.AddJsonFile(@"appsettings.json").Build();
        using (SqliteConnection connection = new SqliteConnection(config.GetConnectionString("ASPNETBlogContext")))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = commandString;
                if (commandString[0].ToString().ToLower() != "s")
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception("Query could not run!: " + ex);
                    }
                    return itemList;
                }
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            itemList.Add(
                                Filler(reader)
                            );
                        }
                    }
                }
                connection.Close();
            }
        }
        return itemList;
    }
}