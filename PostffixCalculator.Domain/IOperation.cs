namespace PostfixCalculator.Domain
{
    /// <summary>
    /// Интерфейс для операции
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Арность операции
        /// </summary>
        int Arity { get; }
        /// <summary>
        /// Приоритет
        /// </summary>
        int Priority { get; }
        /// <summary>
        /// Строковое представление операции
        /// </summary>
        string StringPresentation { get; }
        /// <summary>
        /// Выполнить операцию над аргументами
        /// </summary>
        /// <param name="arguments"> массив численных аргументов</param>
        /// <returns></returns>
        double Perform(params double[] arguments);
    }
}