namespace PostfixCalculator.Application
{
    using System;

    public class UnrecognizedOperationException : Exception
    {
        public override string Message
        {
            get
            {
                return "Неопознанный оператор в строке выражения";
            }
        }
    }
}