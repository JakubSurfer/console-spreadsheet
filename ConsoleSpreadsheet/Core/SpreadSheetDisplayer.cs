using System;
using ConsoleSpreadsheet.Model;

namespace ConsoleSpreadsheet.Core
{
    public class SpreadSheetDisplayer 
    {
        public static void Display(Spreadsheet spreadsheet)
        {
            var key = 1;
            var output = "";
            foreach (var cell in spreadsheet)
            {
                if (key != cell.Key[1])
                    Console.Write("\n");

                Console.Write(cell.Value.Value.ToString() + " ");
                key = cell.Key[1];
            }
            Console.ReadLine();
        }
    }

    public interface ISpreadSheetDisplayer
    {
        void Display(Spreadsheet spreadsheet);
    }
}
