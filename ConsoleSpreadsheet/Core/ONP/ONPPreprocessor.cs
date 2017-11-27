using System.Text.RegularExpressions;

namespace ConsoleSpreadsheet.Core.ONP
{
    public interface IONPPreprocessor
    {
        string Prepare(string expression);
    }

    public class ONPPreprocessor : IONPPreprocessor
    {
        public string Prepare(string expression)
        {
            return HandleExpressionSigns(expression);
        }

        private static string HandleExpressionSigns(string expression)
        {
            var preprocessed = Regex.Replace(expression, @"[\+|\*|\/]", " $& ");
            var withMinuses = Regex.Replace(preprocessed, @"(?<=\d)-", " $& ");
            return withMinuses;
        }
    }
}
