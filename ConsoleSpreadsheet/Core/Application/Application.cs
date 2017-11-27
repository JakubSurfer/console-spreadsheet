namespace ConsoleSpreadsheet.Core.Application
{
    public class Application
    {
        private readonly IInputDataReader _inputDataReader;
        private readonly ISpreadsheetFactory _spreadsheetFactory;

        public Application(IInputDataReader inputDataReader, ISpreadsheetFactory spreadsheetFactory)
        {
            _inputDataReader = inputDataReader;
            _spreadsheetFactory = spreadsheetFactory;
        }

        public void Run()
        {
            var rawInputData = _inputDataReader.GetInputData();
            var spreadSheet = _spreadsheetFactory.Create(rawInputData);
            SpreadSheetDisplayer.Display(spreadSheet);
        }
    }
}