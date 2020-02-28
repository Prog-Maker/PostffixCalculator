namespace PostfixCalculator.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using global::PostfixCalculator.Domain;

    /// <summary>
    /// Распознаватель операций и операндов
    /// </summary>
    public class BaseRecognizer : IRecognizer
    {
        private IEnumerable<IOperation> operations;

        public BaseRecognizer() { }

        public BaseRecognizer(IEnumerable<IOperation> operationsList)
        {
            operations = operationsList;
        }

        public string GetFullNumber(string expression)
        {
            var recognizedNumber = new StringBuilder();
            foreach (var character in expression)
            {
                if (char.IsDigit(character) || character == '.' || character == ',')
                {
                    recognizedNumber.Append(character);
                }
                else
                {
                    return recognizedNumber.ToString();
                }
            }
            return recognizedNumber.ToString();
        }

        public string GetFullOperation(string expression)
        {
            foreach (var operation in operations)
            {
                var operatorLength = operation.StringPresentation.Length;
                string operationSubstring;
                try
                {
                    operationSubstring = expression.Substring(0, operatorLength);
                }
                catch (Exception)
                {
                    continue;
                }
                if (operationSubstring == operation.StringPresentation)
                {
                    var recognizedOperation = operationSubstring;
                    return recognizedOperation;
                }
            }
            throw new UnrecognizedOperationException();
        }

        public List<string> Recognize(string expression)
        {
            expression = expression.Replace(" ","");
            
            var operands = new List<string>();

            //var opSeparator = operations.Select(op => op.StringPresentation).ToArray();

            //operands = expression.Split(opSeparator, StringSplitOptions.RemoveEmptyEntries).ToList();

            int nextCharacterPosition = 0;
            for (var i = 0; i < expression.Length; i += nextCharacterPosition)
            {
                //var ch = expression[i];
                //if (char.IsWhiteSpace(expression[i])) continue;

                var recognizedOperand = GetRecognizedOperand(expression[i], expression, i);

                operands.Add(recognizedOperand);
                nextCharacterPosition = recognizedOperand.Length;
            }
            return operands;
        }

        private string GetRecognizedOperand(char ch, string expression, int startIndex)
        {
            string recognizedOperand;

            if (ch == '(' || ch == ')')
            {
                recognizedOperand = ch.ToString();
            }
            else
            {
                recognizedOperand = char.IsDigit(ch) ? 
                    GetFullNumber(expression.Substring(startIndex, expression.Length - startIndex)) 
                    : GetFullOperation(expression.Substring(startIndex, expression.Length - startIndex));
            }

            return recognizedOperand;
        }

        private void SetOperations(IEnumerable<IOperation> operations)
        {
            this.operations = operations;
        }

        public IOperation GetOperation(string operand)
        {
            foreach (var operation in operations.Where(operation => operand == operation.StringPresentation))
            {
                return operation;
            }
            throw new UnrecognizedOperationException();
        }

        public int GetOperationPriority(string operationString)
        {
            var operation = GetOperation(operationString);
            return operation.Priority;
        }

        public IEnumerable<string> GetAvailableOperations()
        {
            return operations.OrderByDescending(o => o.StringPresentation.Length).Select(operation => operation.StringPresentation).ToList();
        }


        public IRecognizer CraeteOperations(IEnumerable<IOperation> operations)
        {
             SetOperations(operations);
             return this;
        }
    }
}