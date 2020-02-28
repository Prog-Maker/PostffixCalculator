namespace BaseOperations
{
    using PostfixCalculator.Domain;
    using System;
    public class Sqrt : IOperation
    {
        public int Arity => 1;

        public int Priority => 1;

        public string StringPresentation => "sqrt";

        public double Perform(params double[] arguments)
        {
            return Math.Sqrt(arguments[0]);
        }
    }
}
