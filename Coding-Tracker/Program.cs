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
        CreateDB createDB = new();
        createDB.CreateTable(connectionstring);

        UserInput input = new UserInput();
        input.GetUserInput();
    }
}