using System.Text.RegularExpressions;
using ConsoleSpreadsheet.Model;

namespace ConsoleSpreadsheet.Core
{
    public interface IExpressionProcessor
    {
        void Process(Spreadsheet spreadsheet, Cell cell);
    }

    public class ExpressionProcessor : IExpressionProcessor
    {
        public void Process(Spreadsheet spreadsheet, Cell cell)
        {
            if (HasPredifinedValue(cell))
            {
                cell.Expression.IsFetched = true;
                cell.Value = double.Parse(cell.Expression.Content);
                return;
            }

            var matches = Regex.Matches(cell.Expression.Content, @"[A-Z]\d+");

            foreach (var match in matches)
            {
                var cellKey = match.ToString();
                if (!spreadsheet.ContainsKey(cellKey))
                {
                    cell.Expression.Content = Regex.Replace(cell.Expression.Content, cellKey, "0");
                    return;
                }

                var replacer = spreadsheet[cellKey];

                if (replacer == cell)
                {
                    cell.Value = 0;
                    cell.Expression.Content = "0";
                    cell.Expression.IsFetched = true;
                    return;
                }

                if (replacer.Expression.IsFetched)
                    cell.Expression.Content = Regex.Replace(cell.Expression.Content, cellKey, replacer.Expression.Content);
                else
                {
                    Process(spreadsheet, replacer);
                    cell.Expression.Content = Regex.Replace(cell.Expression.Content, cellKey, replacer.Expression.Content);
                }
            }
        }

        private bool HasPredifinedValue(Cell cell)
        {
            return double.TryParse(cell.Expression.Content, out _);
        }
    }
}
