using System.Globalization;
using System.Linq;

namespace ConsoleSpreadsheet.Core
{
    public static class StringExtensions
    {
        public static string RemoveWhiteSpaces(this string input)
        {
            return string.Concat(input.Where(x => !char.IsWhiteSpace(x)));
        }

        public static string RemoveLastChar(this string input)
        {
            return input.Remove(input.Length-1);
        }

        public static double ConvertToDouble(this string value)
        {
            var format = new NumberFormatInfo { NegativeSign = "-" };
            return double.Parse(value, format);
        }
    }
}