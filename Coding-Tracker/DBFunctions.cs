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
using System.Globalization;

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
            string StartTime = GetDateFunc("StartTime");

            string EndTime = GetDateFunc("EndTime");

            while(!(Convert.ToDateTime(StartTime) < Convert.ToDateTime(EndTime)))
            {
                Console.WriteLine("End Time can not be older than Start Time. Please re-enter the End Time accordingly.");
                EndTime = GetDateFunc("EndTime");
            }

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

            string StartTime = GetDateFunc("StartTime");

            string EndTime = GetDateFunc("EndTime");

            while (!(Convert.ToDateTime(StartTime) < Convert.ToDateTime(EndTime)))
            {
                Console.WriteLine("End Time can not be older than Start Time. Please re-enter the End Time accordingly.");
                EndTime = GetDateFunc("EndTime");
            }

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

        public string GetDateFunc(string whichdate)
        {
            Console.WriteLine($"Please insert the {whichdate}");
            var inputDate = Console.ReadLine();
            while (!DateTime.TryParseExact(inputDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("Please enter the date in yyyy-MM-dd HH:mm:ss format.");
                inputDate = Console.ReadLine();
            }
            return inputDate;
        }
    }
}
