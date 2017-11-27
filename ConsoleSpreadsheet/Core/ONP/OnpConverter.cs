using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleSpreadsheet.Core.ONP
{
    public interface IOnpConverter
    {
        string Convert(string rawExpression);
    }

    public class OnpConverter : IOnpConverter
    {
        private readonly IONPPreprocessor _preprocessor;

        public OnpConverter(IONPPreprocessor preprocessor)
        {
            _preprocessor = preprocessor;
        }

        public string Convert(string rawExpression)
        {
            var expression = _preprocessor.Prepare(rawExpression);

            var onpExpression = new StringBuilder();
            var operators = new Stack<char>();
            var arguments = Regex.Split(expression, @"\s+");

            foreach (var item in arguments)
            {
                if (IsOperator(item))
                {
                    var mathOperator = char.Parse(item);
                    switch (mathOperator)
                    {
                        case '-':
                        case '+':
                        case '*':
                        case '/':
                            if (ShouldPushOperator(operators, mathOperator))
                            {
                                operators.Push(mathOperator);
                                break;
                            }

                            while (ShouldTakeLowerOperator(operators, mathOperator))
                            {
                                onpExpression.Append($" {operators.Pop()}");
                            }

                            operators.Push(mathOperator);
                            break;
                    }
                }
                else
                {
                    onpExpression.Append($" {item}");
                }
            }

            while (operators.Count != 0)
            {
                onpExpression.Append($" {operators.Pop()}");
            }

            onpExpression = onpExpression.Remove(0, 1);
            return onpExpression.ToString();
        }

        private static bool ShouldTakeLowerOperator(Stack<char> operators, char mathOperator)
        {
            return operators.Count > 0 && GetOperationPriority(operators.Peek()) > GetOperationPriority(mathOperator);
        }

        private static bool ShouldPushOperator(Stack<char> operators, char mathOperator)
        {
            return operators.Count == 0 || GetOperationPriority(operators.Peek()) < GetOperationPriority(mathOperator);
        }

        private static bool IsOperator(string item)
        {
            return Regex.IsMatch(item, @"^[\+|\*|\-/]$");
        }

        private static int GetOperationPriority(char sign)
        {
            switch (sign)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    throw new ArgumentException("Invalid operator");
            }

        }
    }
}
