using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Microsoft.Data.Sqlite;
using Coding_Tracker.Models;
using System.Configuration;

namespace Coding_Tracker
{
    internal class DBFunctions
    {
        static string connectionstring = ConfigurationManager.AppSettings.Get("connectionString");
        public List<CodingSession> GetAllRecords()
        {
            List<CodingSession> items = new List<CodingSession>();

            using(var connection = new SqliteConnection(connectionstring))
            {
                using(var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText =
                        @"SELECT * FROM coding_tracker";
                    using(var  reader = command.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                items.Add(new CodingSession
                                {
                                    Id = reader.GetInt32(0),
                                    StartTime = Convert.ToDateTime(reader.GetString(1)),
                                    EndTime = Convert.ToDateTime(reader.GetString(2)),
                                    Duration = reader.GetString(3)
                                });
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Records found, please insert some.");
                        }
                    }
                }
            }
            VisualizationSpectre.ShowTable(items);
            return items;
        }
        public void InsertRecord()
        {
            Console.WriteLine("Please inset starttime");
            string StartTime = Console.ReadLine();

            Console.WriteLine("Please inset endtime");
            string EndTime = Console.ReadLine();

            string Duration = Convert.ToString((Convert.ToDateTime(EndTime) - Convert.ToDateTime(StartTime)).TotalMinutes);

            using(var connection = new SqliteConnection(connectionstring))
            {
                using(var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText =
                        $"INSERT into coding_tracker(StartTime, EndTime, Duration) VALUES ('{StartTime}','{EndTime}','{Duration}')";
                    command.ExecuteNonQuery();
                }
            }
        }
        public void EditRecord()
        {
            Console.WriteLine("Edited the records");
        }
        public void DeleteRecord()
        {
            Console.WriteLine("Deleted the records");
        }
    }
}
