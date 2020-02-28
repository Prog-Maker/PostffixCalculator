namespace BaseOperations
{
    using PostfixCalculator.Domain;
    public class Minus : IOperation
    {
        public double Perform(params double[] arguments)
        {
            return arguments[1] - arguments[0];
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
                return "-";
            }
        }
    }
}
