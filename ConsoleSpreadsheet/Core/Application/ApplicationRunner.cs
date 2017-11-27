using ConsoleSpreadsheet.IoC;
using StructureMap;

namespace ConsoleSpreadsheet.Core.Application
{
    class ApplicationRunner
    {
        public static void Run()
        {
            var container = Container.For<ConsoleRegistry>();
            var app = container.GetInstance<Application>();
            app.Run();
        }
    }
}
