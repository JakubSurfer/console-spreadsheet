using ConsoleSpreadsheet.Model;

namespace ConsoleSpreadsheet.Core.ONP
{
    public interface IOnpExpressionHandler
    {
        void Handle(Spreadsheet spreadSheet);
    }
    public class OnpExpressionHandler : IOnpExpressionHandler
    {

        private readonly IOnpConverter _converter;
        private readonly IOnpProcessor _processor;

        public OnpExpressionHandler(IOnpConverter converter, IOnpProcessor processor)
        {
            _converter = converter;
            _processor = processor;
        }

        public void Handle(Spreadsheet spreadSheet)
        {
            foreach (var cell in spreadSheet)
            {
                cell.Value.Expression.Content = _converter.Convert(cell.Value.Expression.Content);
                cell.Value.Value = _processor.Process(cell.Value.Expression.Content);
            }
        }
    }

    
}
