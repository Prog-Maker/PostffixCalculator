namespace BaseOperations
{
    using PostfixCalculator.Domain;
    using System.Collections.Generic;
    using System.Linq;

    public class Plus : IOperation
    {
        public double Perform(params double[] arguments)
        {
            return ((IEnumerable<double>)arguments).Sum();
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
                return 4;
            }
        }

        public string StringPresentation
        {
            get
            {
                return "+";
            }
        }
    }
}
