using Coding_Tracker.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//USING PURE ADO.NET FOR CRUD

namespace Coding_Tracker
{
    internal class OldDBFunctions
    {
        static string connectionstring = ConfigurationManager.AppSettings.Get("connectionString");
        public List<CodingSession> GetAllRecords()
        {
            var items = new List<CodingSession>();
            using (var connection = new SqliteConnection(connectionstring))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText =
                        @"SELECT * FROM coding_tracker";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
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

            using (var connection = new SqliteConnection(connectionstring))
            {
                using (var command = connection.CreateCommand())
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
            Console.WriteLine("Please enter the id which you want to edit");
            string ID = Console.ReadLine();

            Console.WriteLine("Please insert starttime");
            string StartTime = Console.ReadLine();

            Console.WriteLine("Please inset endtime");
            string EndTime = Console.ReadLine();

            string Duration = Convert.ToString((Convert.ToDateTime(EndTime) - Convert.ToDateTime(StartTime)).TotalMinutes);


            using (var connection = new SqliteConnection(connectionstring))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText =
                        $"UPDATE coding_tracker SET StartTime = '{StartTime}', EndTime = '{EndTime}', Duration = '{Duration}' WHERE Id = {ID}";

                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteRecord()
        {
            Console.WriteLine("Please enter the Id you want to delete");
            string Id = Console.ReadLine();

            using (var connection = new SqliteConnection(connectionstring))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText =
                        $"DELETE FROM coding_tracker WHERE Id = {Id}";
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
