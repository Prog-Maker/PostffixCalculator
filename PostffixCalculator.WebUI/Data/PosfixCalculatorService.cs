namespace PostffixCalculator.WebUI.Data
{
    using PostfixCalculator.Domain;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::PostfixCalculator.Application;
    public class PosfixCalculatorService
    {

        IRecognizer recognizer;
        PostfixCalculatorCreator creator;
        PostfixCalculator calculator;

        public PosfixCalculatorService(IRecognizer recognizer)
        {
            this.recognizer = recognizer;
            creator = new PostfixCalculatorCreator(recognizer, WayToGetOperation.Internal);
            calculator = creator.CreateCalculator();
        }

        public Task<string> Calculate(string expression)
        {
            return Task.Run(() =>
            {
                try
                {
                    return calculator.Calculate(expression).ToString();
                }
                catch (UnrecognizedOperationException e)
                {
                    return e.Message;
                }
                catch(Exception ex)
                {
                    return "Ошибка, возможно задано неправильное количество операндов в выражении!!!";
                }
            });
        }

        public Task<IReadOnlyCollection<IOperation>> GetOperationsAsync()
        {
            return Task.Run(() => creator.Operations);
        }
    }
}
