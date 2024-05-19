using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coding_Tracker.Models;
using Spectre.Console;

namespace Coding_Tracker
{
    internal class VisualizationSpectre
    {
        public static void ShowTable(List<CodingSession> coding)
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("StartTime");
            table.AddColumn("EndTime");
            table.AddColumn("Duration");

            foreach(CodingSession item in coding)
            {
                table.AddRow($"{item.Id}", $"{item.StartTime}", $"{item.EndTime}", $"{item.Duration}");
            }

            AnsiConsole.Write(table);
        }
    }
}
