namespace CalculatorSolution
{
    using BaseOperations;
    using PostfixCalculator.Application;
    using PostfixCalculator.Domain;
    using System;
    using System.Collections.Generic;
    using System.IO;
    class Program
    {
        private static List<IOperation> GetOperations()
        {
            var operations = new List<IOperation>
            {
                new Divide(),
                new Minus(),
                new PlusPlus(),
                new Plus(),
                new Multiply(),
                new Sqrt()
            };
            return operations;
        }


        static void Main()
        {
            // var pluginReader = new OperationPluginReader();
            List<IOperation> operations;
            try
            {
                operations = GetOperations();
               // operations = pluginReader.ReadPluginsFrom(Environment.CurrentDirectory + "\\Plugins");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Ошибка! Отсутсвует директория Plugins");
                Console.WriteLine("Нажмите любую клавишу, чтобы выйти...");
                Console.ReadKey();
                return;
            }
            var recognizer = new BaseRecognizer();
            var calculator = new PostfixCalculator(recognizer.CraeteOperations(operations));
            ShowAvailableOperations(calculator.GetAvailableOperations());
            var reader = new ConsoleInputReader();
            while (true)
            {
                try
                {
                    var result = calculator.Calculate(reader.GetExpression());
                    Console.WriteLine($"Результат: {result}");
                }
                catch (UnrecognizedOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Нажмите Enter, чтобы продолжить или Esc, чтобы выйти");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

        private static void ShowAvailableOperations(IEnumerable<string> availableOperations)
        {
            Console.WriteLine("Доступны следующие операции:");
            foreach (var availableOperation in availableOperations)
            {
                Console.WriteLine(availableOperation);
            }
        }
    }
}
