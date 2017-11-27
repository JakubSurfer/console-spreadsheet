using System.Collections.Generic;
using ConsoleSpreadsheet.Core.ONP;
using ConsoleSpreadsheet.Model;

namespace ConsoleSpreadsheet.Core
{
    public interface ISpreadsheetFactory
    {
        Spreadsheet Create(IEnumerable<string[]> rawData);
    }

    public class SpreadSheetFactory : ISpreadsheetFactory
    {
        public const char InitialRowIndex = '1';
        public const char InitialColumnLabel = 'A';

        private readonly IExpressionProcessor _expressionProcessor;
        private readonly IOnpExpressionHandler _expressionHandler;

        public SpreadSheetFactory(IExpressionProcessor expressionProcessor, IOnpExpressionHandler expressionHandler)
        {
            _expressionProcessor = expressionProcessor;
            _expressionHandler = expressionHandler;
        }

        public Spreadsheet Create(IEnumerable<string[]> rawData)
        {
            var spreadsheet = CreateRawSpreadsheet(rawData);
            ProcessCellExpressions(spreadsheet);
            _expressionHandler.Handle(spreadsheet);
            return spreadsheet;
        }

        private void ProcessCellExpressions(Spreadsheet spreadsheet)
        {
            foreach (var cell in spreadsheet)
            {
                _expressionProcessor.Process(spreadsheet, cell.Value);
            }
        }

        private static Spreadsheet CreateRawSpreadsheet(IEnumerable<string[]> rawData)
        {
            var spreadSheet = new Spreadsheet();
            var rowIndex = InitialRowIndex;

            foreach (var row in rawData)
            {
                var columnLabel = InitialColumnLabel;
                foreach (var rawCell in row)
                {
                    var key = CreateCellKey(columnLabel, rowIndex);
                    var cell = Cell.Create(rawCell);
                    spreadSheet.Add(key, cell);
                    columnLabel++;
                }
                rowIndex++;
            }
            spreadSheet.Columns = rowIndex;
            return spreadSheet;
        }

        private static string CreateCellKey(char columnLabel, char rowIndex)
        {
            return columnLabel + rowIndex.ToString();
        }
    }
}
