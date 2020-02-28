namespace BaseOperations
{
    using PostfixCalculator.Domain;
    public class PlusPlus : IOperation
    {
        public int Arity => 1;

        public int Priority => 3;

        public string StringPresentation => "++";

        public double Perform(params double[] arguments)
        {
            return ++arguments[0];
        }
    }
}
