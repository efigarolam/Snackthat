using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Clase encargada de la conexión con la base de datos y la llamada a los procedimientos almacenados.
/// </summary>
public class ActiveRecord
{
    /// <summary>
    /// Property to initialize the connection string to the database.
    /// </summary>
    private string connection;

    /// <summary>
    /// Property to initialize the sql string to execute on the database server.
    /// </summary>
    private string sql;
    
    /// <summary>
    /// Object to return the rows of the executed query.
    /// </summary>
    private DataTable query;

    /// <summary>
    /// Constructor of the class, initialize the connection string.
    /// </summary>
    public ActiveRecord()
	{
        MySqlConnection connection = new MySqlConnection("server = localhost; user id = root; database = snackthatseller; Allow Zero Datetime=false;Convert Zero Datetime=True");
        this.connection = connection.ConnectionString;
	}

    /// <summary>
    /// Method to call a stored procedure and return the rows returned by itsself.
    /// </summary>
    /// <param name="procedure">Name of the stored procedure to be executed</param>
    /// <param name="parameters">ArrayList of all the parameters to be send to the stored procedure</param>
    /// <returns>Returns a DataTable with the rows of the query</returns>
    public DataTable callProcedure(string procedure, ArrayList parameters = null)
    {
        this.sql = "call " + procedure + "(";

        if (parameters != null)
        {
            for (int i = 0; i <= parameters.Count - 1; i++)
            {
                if (i <= parameters.Count - 2)
                {
                    if (parameters[i] != null)
                        this.sql += "'" + parameters[i].ToString() + "', ";
                    else this.sql += "null,";
                }
                else
                {
                    if (parameters[i] != null)
                        this.sql += "'" + parameters[i].ToString() + "') ";
                    else this.sql += "null)";
                }

            }
        }
        else
        {
            this.sql += ")";
        }

        try
        {
            using (MySqlConnection connect = new MySqlConnection(this.connection))
            {
                MySqlCommand cmd = new MySqlCommand(this.sql, connect);
                cmd.Connection.Open();

                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                this.query = new DataTable();
                this.query.Load(reader);
                reader.Close();
                cmd.Connection.Close();

                return this.query;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}