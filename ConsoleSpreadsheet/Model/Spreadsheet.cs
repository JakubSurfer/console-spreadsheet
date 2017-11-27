using System.Collections.Generic;

namespace ConsoleSpreadsheet.Model
{
    public class Spreadsheet : Dictionary<string, Cell>
    {
        public int Rows { get; set; }
        public char Columns { get; set; }
    }
}
