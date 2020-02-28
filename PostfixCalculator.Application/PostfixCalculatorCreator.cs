namespace PostfixCalculator.Application
{
    using global::PostfixCalculator.Domain;
    using BaseOperations;
    using System.Collections.Generic;
    using System;

    public enum WayToGetOperation
    {
        Internal, External
    }

    public class PostfixCalculatorCreator
    {
        private List<IOperation> GetAllOperations()
        {
            return new List<IOperation>
            {
                new Divide(),
                new Multiply(),
                new Plus(),
                new PlusPlus(),
                new Sqrt(),
                new Minus()
            };
        }


        private List<IOperation> operations;
        private IRecognizer recognizer;
        private IPluginReader pluginReader;

        public PostfixCalculatorCreator(IRecognizer recognizer,
                                        WayToGetOperation wayToGet,
                                        IPluginReader pluginReader = null)
        {
            this.recognizer = recognizer;
            this.pluginReader = pluginReader;
            if (pluginReader != null) this.pluginReader = pluginReader;

            operations = new List<IOperation>();

            GetOperationsFromWay(wayToGet);
        }

        private void GetOperationsFromWay(WayToGetOperation wayToGetOperation)
        {
            switch (wayToGetOperation)
            {
                case WayToGetOperation.Internal:
                    operations = GetAllOperations();
                    break;
                case WayToGetOperation.External:
                    if (pluginReader != null)
                    {
                        operations = pluginReader
                                .ReadPluginsFrom(Environment.CurrentDirectory + "\\Plugins");
                    }
                    else
                    {
                        throw new ArgumentNullException("PluginReader instance is null");
                    }
                    break;
            }
        }

        public IReadOnlyCollection<IOperation> Operations => operations;


        public PostfixCalculator CreateCalculator()
        {
            var calculator = new PostfixCalculator(recognizer.CraeteOperations(Operations));

            return calculator;
        }
    }
}
