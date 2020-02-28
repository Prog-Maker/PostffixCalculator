namespace PostfixCalculator.Application
{
    using global::PostfixCalculator.Domain;
    using System.Collections.Generic;
    using System.Globalization;


    /// <summary>
    /// Калькулятор, основанный на постфиксной нотации
    /// </summary>
    public class PostfixCalculator : ICalculator
    {
        private readonly NumberFormatInfo numberFormatInfo;
        NumberStyles styles;
        private readonly IRecognizer recognizer;


        public PostfixCalculator(IRecognizer recognizer)
        {
            numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
            styles = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;
            this.recognizer = recognizer;
        }

        /// <summary>
        /// Расчитывает выражение и выдает результат
        /// </summary>
        /// <param name="expression">строка выражения</param>
        /// <returns></returns>
        public double Calculate(string expression)
        {
            var operands = recognizer.Recognize(expression);
            var operandsQueue = ReorderInPostfixNotation(operands);
            var result = PerformCalculations(operandsQueue);
            return result;
        }

        /// <summary>
        /// Выполняет соответсвующие действия над операндами, в зависимости от их типа
        /// </summary>
        /// <param name="operandsQueue">очередь операндов, сформированная в постфиксной нотации</param>
        /// <returns></returns>
        private double PerformCalculations(Queue<string> operandsQueue)
        {
            double result = 0;
            var helperStack = new Stack<string>();
            while (operandsQueue.Count != 0)
            {
                var operand = operandsQueue.Dequeue();
                if (IsNumber(operand))
                {
                    helperStack.Push(operand);
                }
                else
                {
                    result = PerformOperation(operand, helperStack);
                }
            }
            return result;
        }

        /// <summary>
        /// Выполняет конкретную операцию над соответсвующим количеством операндов
        /// </summary>
        /// <param name="operand">строковое представление операции</param>
        /// <param name="helperStack">вспомогательный стек операндов</param>
        /// <returns></returns>
        private double PerformOperation(string operand, Stack<string> helperStack)
        {
            var currentOperation = recognizer.GetOperation(operand);
            var numberOfArguments = currentOperation.Arity;
            var arguments = new double[numberOfArguments];
            //try
            //{
            for (var i = 0; i < numberOfArguments; i++)
            {
                arguments[i] = double.Parse(helperStack.Pop(), styles, numberFormatInfo);
            }
            //}
            //catch
            //{
            //    throw new UnrecognizedOperationException();
            //}
            var result = currentOperation.Perform(arguments);
            helperStack.Push(result.ToString(numberFormatInfo));
            return result;
        }

        /// <summary>
        /// Оперделяет, является ли входная строка числом
        /// </summary>
        /// <param name="operand">строка операнда</param>
        /// <returns></returns>
        private bool IsNumber(string operand)
        {
            var isNumber = double.TryParse(operand,
                                           NumberStyles.AllowDecimalPoint, numberFormatInfo,
                                           out double _);
            return isNumber;
        }

        /// <summary>
        /// Строит из операндов очередь в соответствии с постфиксной нотацией
        /// </summary>
        /// <param name="operands">массив операндов</param>
        /// <returns>очередь в постфиксной нотации</returns>
        public Queue<string> ReorderInPostfixNotation(List<string> operands)
        {
            var resultQueue = new Queue<string>();
            var helperStack = new Stack<string>();

            int counter;
            for (counter = 0; counter < operands.Count;)
            {
                var operand = operands[counter];

                if (IsNumber(operand))
                {
                    resultQueue.Enqueue(operand);
                }
                else
                {
                    if (operand == "(")
                    {
                        FindInnerParentheses(operands, ref counter, resultQueue);

                        continue;
                    }

                    if (helperStack.Count > 0)
                    {
                        if (GetPriority(operand) <= GetPriority(helperStack.Peek()))
                            helperStack.Push(operand);
                        else
                        {
                            while (helperStack.Count > 0 && GetPriority(operand) > GetPriority(helperStack.Peek()))
                                resultQueue.Enqueue(helperStack.Pop());
                            helperStack.Push(operand);
                        }
                    }
                    else
                        helperStack.Push(operand);
                }
                counter++;
            }

            foreach (var operand in helperStack)
            {
                resultQueue.Enqueue(operand);
            }
            return resultQueue;
        }

        private void FindInnerParentheses(List<string> operands, ref int counter, Queue<string> resultQueue)
        {
            var tempOperands = new List<string>();

            for (int i = counter; i < operands.Count; i = counter)
            {
                var op = operands[i];

                // foreach (var op in operands.Skip(++counter))
                if (op == "(")
                {
                    counter++;
                    FindInnerParentheses(operands, ref counter, resultQueue);
                    continue;
                }
                if (op == ")")
                {
                    counter++;
                    break;
                }
                tempOperands.Add(op);
                counter++;
            }

            var tempResultQueue = ReorderInPostfixNotation(tempOperands);

            foreach (var op in tempResultQueue)
            {
                resultQueue.Enqueue(op);
            }
        }

        private int GetPriority(string operation)
        {
            return recognizer.GetOperationPriority(operation);
        }

        public IEnumerable<string> GetAvailableOperations()
        {
            return recognizer.GetAvailableOperations();
        }
    }
}