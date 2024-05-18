using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Tracker
{
    internal class UserInput
    {
        public void GetUserInput()
        {
            Console.Clear();
            DBFunctions dBFunctions = new DBFunctions();

            bool stop = false;
            while(!stop)
            {
                Console.WriteLine("Main Meny");
                Console.WriteLine("press 0 to exit");
                Console.WriteLine("press 1 to view all records");
                Console.WriteLine("press 2 to to insert a record");
                Console.WriteLine("press 3 to edit a record");
                Console.WriteLine("press 4 to delete a record");

                string? command = Console.ReadLine();

                switch (command)
                {
                    case "0":
                        stop = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        dBFunctions.GetAllRecords();
                        break;
                    case "2":
                        dBFunctions.InsertRecord();
                        break;
                    case "3":
                        dBFunctions.EditRecord();
                        break;
                    case "4":
                        dBFunctions.DeleteRecord();
                        break;
                    default:
                        Console.WriteLine("Please enter a number between 0 - 4");
                        break;
                }
            }
        }
    }
}
