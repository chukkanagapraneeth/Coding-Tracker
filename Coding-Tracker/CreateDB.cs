using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker
{
    internal class CreateDB
    {
        public void CreateTable(string connectionstring)
        {
            using(var connection = new SqliteConnection(connectionstring))
            {
                using(var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText =
                        @"CREATE TABLE IF NOT EXISTS coding_tracker (Id INTEGER PRIMARY KEY AUTOINCREMENT, StartTime TEXT, EndTime TEXT, Duration TEXT)";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
