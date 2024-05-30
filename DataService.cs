using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

public class DataService
{
    public DataService()
    {
    }

    public static void ExecuteCommand(string query)
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = "<dbname>.database.windows.net";
        builder.UserID = "<username>";
        builder.Password = "<Password>";
        builder.InitialCatalog = "sqlforopenai";

        var connectionString = builder.ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    public static List<List<string>> GetDataTable(string query)
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = "sqlforopenai.database.windows.net";
        builder.UserID = "barut";
        builder.Password = "Deneme!12345";
        builder.InitialCatalog = "sqlforopenai";

        var rows = new List<List<string>>();
        var connectionString = builder.ConnectionString;
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    bool headersAdded = false;

                    while (reader.Read())
                    {
                        var cols = new List<string>();
                        var headerCols = new List<string>();

                        if (!headersAdded)
                        {
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                headerCols.Add(reader.GetName(i).ToString());
                            }
                            rows.Add(headerCols);
                            headersAdded = true;
                        }

                        var row = new List<string>();
                        for (var i = 0; i <= reader.FieldCount - 1; i++)
                        {
                            cols.Add(reader.GetValue(i).ToString());
                        }
                        rows.Add(cols);
                    }
                }
            }
        }
        return rows;
    }
}
