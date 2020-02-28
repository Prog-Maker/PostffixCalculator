namespace BaseOperations
{
    using PostfixCalculator.Domain;
    using System;
    public class Multiply : IOperation
    {
        public double Perform(params double[] arguments)
        {
            double d = arguments[1] * arguments[0];
            if (!double.IsInfinity(d))
                return d;
            throw new OverflowException("Слишком большие числа");
        }

        public int Arity
        {
            get
            {
                return 2;
            }
        }

        public int Priority
        {
            get
            {
                return 3;
            }
        }

        public string StringPresentation
        {
            get
            {
                return "*";
            }
        }
    }
}
