using PostfixCalculator.Application;
using PostfixCalculator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostffixCalculator.WebUI.Data
{
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
            try
            {
                return Task.Run(() => calculator.Calculate(expression).ToString());
            }
            catch (UnrecognizedOperationException e)
            {
                return Task.Run(() =>e.Message);
            }
            catch(Exception ex)
            {
                return Task.Run(() => ex.Message);
            }
        }

        public Task<IReadOnlyCollection<IOperation>> GetOperationsAsync()
        {
            return Task.Run(() => creator.Operations);
        }
    }
}
