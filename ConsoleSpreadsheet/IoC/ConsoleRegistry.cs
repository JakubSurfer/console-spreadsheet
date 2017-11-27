using ConsoleSpreadsheet.Core;
using ConsoleSpreadsheet.Core.ONP;
using StructureMap;

namespace ConsoleSpreadsheet.IoC
{
    public class ConsoleRegistry : Registry
    {
        public ConsoleRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });

            For<IInputDataReader>().Use<InputDataReader>();
            For<ISpreadsheetFactory>().Use<SpreadSheetFactory>();
            For<IExpressionProcessor>().Use<ExpressionProcessor>();
            For<IOnpExpressionHandler>().Use<OnpExpressionHandler>();
            For<IONPPreprocessor>().Use<ONPPreprocessor>();
            For<IOnpConverter>().Use<OnpConverter>();
            For<IOnpProcessor>().Use<OnpProcessor>();
        }
    }
}
