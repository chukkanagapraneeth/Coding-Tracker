using System;
using System.Configuration;
using Microsoft.Data.Sqlite;
using Spectre;

namespace Coding_Tracker;

class Program
{
    static string connectionstring = ConfigurationManager.AppSettings.Get("connectionString");

    static void Main()
    {
        using(var connection = new SqliteConnection(connectionstring))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText =
                    @"CREATE TABLE IF NOT EXISTS coding_tracker (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, Duration TEXT)";
                command.ExecuteNonQuery();
            }
        }
    }
}