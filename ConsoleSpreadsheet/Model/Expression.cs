namespace ConsoleSpreadsheet.Model
{

    public class Expression
    {
        public string Content { get; set; }
        public bool IsFetched { get; set; }

        public Expression(string expression)
        {
            Content = expression;
        }
    }
}
