using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleSpreadsheet.Core.ONP
{
    public interface IOnpProcessor
    {
        double Process(string input);
    }

    public class OnpProcessor : IOnpProcessor
    {
        public double Process(string input)
        {
            var arguments = GetExpressionArguments(input);
            var stack = new Stack<double>();

            foreach (var item in arguments)
            {
                if (IsOperator(item))
                {
                    stack.Push(CalculateOperation(item, stack));
                }
                else
                {
                    stack.Push(item.ConvertToDouble());
                }
            }
            return stack.Peek();
        }

        private static double CalculateOperation(string item, Stack<double> stack)
        {
            switch (Convert.ToChar(item))
            {
                case '-':
                    return Calculate((x1, x2) => x2 - x1, stack);
                case '+':
                    return Calculate((x1, x2) => x2 + x1, stack);
                case '*':
                    return Calculate((x1, x2) => x2 * x1, stack);
                case '/':
                    return Calculate((x1, x2) => x2 / x1, stack);
            }
            return 0;
        }

        private static double Calculate(Func<double, double, double> mathFunction, Stack<double> stack)
        {
            var first = double.Parse(stack.Pop().ToString());
            var second = double.Parse(stack.Pop().ToString());

            return mathFunction(first, second);
        }

        private static bool IsOperator(string sym)
        {
            return Regex.IsMatch(sym, @"^[\+|\*|\-|\/]$");
        }

        private static string[] GetExpressionArguments(string input)
        {
            return Regex.Split(input, @"\s+");
        }
    }
}
