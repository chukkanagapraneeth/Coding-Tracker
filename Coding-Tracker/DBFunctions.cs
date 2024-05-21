using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Microsoft.Data.Sqlite;
using Coding_Tracker.Models;
using System.Configuration;
using Dapper;

//USING DAPPER FOR CRUD

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

                string SqlCommand = "SELECT * FROM coding_tracker;";
                items = connection.Query<CodingSession>(SqlCommand).ToList();
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
                var SqlCommand = "INSERT INTO coding_tracker (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";
                var anonObject = new {StartTime =  StartTime, EndTime = EndTime, Duration = Duration};
                connection.Execute(SqlCommand, anonObject);
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
                var SqlCommand = "UPDATE coding_tracker SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration WHERE Id = @Id";
                var anonObject = new { Id = ID, StartTime = StartTime, EndTime = EndTime, Duration = Duration};
                connection.Execute(SqlCommand, anonObject);
            }
        }
        public void DeleteRecord()
        {
            Console.WriteLine("Please enter the Id you want to delete");
            string Id = Console.ReadLine();

            using (var connection = new SqliteConnection(connectionstring))
            {
                var SqlCommand = "DELETE FROM coding_tracker WHERE Id = @Id";
                var anonObj = new { Id = Id };
                connection.Execute(SqlCommand, anonObj);
            }
        }
    }
}
