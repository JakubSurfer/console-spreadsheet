using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleSpreadsheet.Core
{
    public interface IInputDataReader
    {
        IEnumerable<string[]> GetInputData();
    }

    class InputDataReader : IInputDataReader
    {
        private const char EndOfSpreadsheetChar = ';';

        public IEnumerable<string []> GetInputData()
        {
            var output = new List<string []>();
            var isInputDone = true;

            while (isInputDone)
            {
                var rawInput = GetRawInput();
                isInputDone = rawInput.LastOrDefault() != EndOfSpreadsheetChar;
                var rawCells = CreateRawCells(rawInput);
                output.Add(rawCells);
            }
            return output;
        }

        private static string[] CreateRawCells(string rawInput)
        {
            return rawInput.RemoveLastChar().ToUpper().Split('|');
        }

        private static string GetRawInput()
        {
            var input = Console.ReadLine();
            var inputWithoutWhitespaces = input.RemoveWhiteSpaces();
            return inputWithoutWhitespaces;
        }
    }
}
