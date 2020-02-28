namespace PostfixCalculator.Domain
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Интерфейс распознавателя выражений
    /// </summary>
    public interface IRecognizer
    {
       IRecognizer CraeteOperations(IEnumerable<IOperation> operations);
        
        /// <summary>
        /// Выделяет строку числа из строки выражения 
        /// </summary>
        /// <param name="expression">строка выражения</param>
        /// <returns></returns>
        string GetFullNumber(string expression);

        /// <summary>
        /// Выделяет строку числа из строки выражения
        /// </summary>
        /// <param name="expression">строка выражения</param>
        /// <returns></returns>
        string GetFullOperation(string expression);

        /// <summary>
        /// Возвращает список распознанных операндов
        /// </summary>
        /// <param name="expression">строка выражения</param>
        /// <returns></returns>
        List<string> Recognize(string expression);


        /// <summary>
        /// По строковому представлению операции возвращает соответсвтующий экземпляр IOperation
        /// </summary>
        /// <param name="operand">строковое представление операции</param>
        /// <returns></returns>
        IOperation GetOperation(string operand);

        /// <summary>
        /// Возвращает приоритет указанной операции
        /// </summary>
        /// <returns></returns>
        int GetOperationPriority(string operationString);

        /// <summary>
        /// Возвращает все доступные операци
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAvailableOperations();
    }
}