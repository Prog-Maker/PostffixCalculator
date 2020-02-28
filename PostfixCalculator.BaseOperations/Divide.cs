namespace BaseOperations
{
    using PostfixCalculator.Domain;
    using System;
    public class Divide : IOperation
    {
        public double Perform(params double[] arguments)
        {
            double d = arguments[1] / arguments[0];
            if (!double.IsInfinity(d))
                return d;
            throw new DivideByZeroException("Попытка деления на ноль");
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
                return 1;
            }
        }

        public string StringPresentation
        {
            get
            {
                return "/";
            }
        }
    }
}
