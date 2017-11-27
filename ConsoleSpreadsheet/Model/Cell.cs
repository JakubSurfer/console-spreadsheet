namespace ConsoleSpreadsheet.Model
{
    public class Cell
    {
        public Expression Expression { get; set; }
        public double Value { get; set; }

        public static Cell Create(string expression)
        {
            return new Cell
            {
                Expression = new Expression(expression)
            };
        }
    }
}
