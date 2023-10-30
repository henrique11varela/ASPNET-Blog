
using ASPNET_Blog.Models;
using Microsoft.Data.Sqlite;

/** How to implement
*
* - Fill private string _TableName with the name of the DB table
* - Implement a "filler" method that uses a reader to fill the props
* - Do not implement an "Id" since it is already implemented
*/
class ModelTemplate
{
    private string _TableName = "";
    private List<string> _TableColumns = new List<string> { };
    public int Id = 0;
    ModelTemplate filler(SqliteDataReader reader){
        return new ModelTemplate();
    }

    /** Stores data into the DataBase
    * 
    */
    void save()
    {
        string command = "";
        if (this.Id > 0)
        {
            command += $"UPDATE {_TableName} WHERE id = {this.Id} (";

        }
        else
        {
            command += $"INSERT INTO {_TableName} (";
        }
        for (int i = 0; i < _TableColumns.Count; i++)
        {
            command += i == 0 ? "" : ", ";
            command += _TableColumns[i];
        }
        command += ") VALUES ();";
    }

    /** Returns the item that has the Id "id"
    * 
    */
    ModelTemplate find(int id)
    {
        string query = $"SELECT * FROM {_TableName} WHERE id = {id}";
        return AccessDB(query, filler)[0];
    }

    /** Returns a List<> of items that pass the "conditions"
    * 
    */
    List<ModelTemplate> all()
    {
        string query = $"SELECT * FROM {_TableName}";
        return AccessDB(query, filler);
    }

    /** Returns a List<> of items that pass the "conditions"
    * 
    */
    List<ModelTemplate> where(string conditions)
    {
        string query = $"SELECT * FROM {_TableName} WHERE {conditions}";
        return AccessDB(query, filler);
    }

    /** Deletes the item that has the Id "id"
    * 
    */
    void delete(int id)
    {
        string query = $"DELETE FROM {_TableName} WHERE id = {id}";
        AccessDB(query, filler);
    }

    /** Manages the connection to the DataBase
    * 
    */
    List<ModelTemplate> AccessDB(string commandString, Func<SqliteDataReader, ModelTemplate> fillerFunction)
    {
        List<ModelTemplate> itemList = new List<ModelTemplate>();
        using (SqliteConnection connection = new SqliteConnection())
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
                    catch (System.Exception)
                    {
                        throw new Exception("Query could not run!");
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
                                fillerFunction(reader)
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