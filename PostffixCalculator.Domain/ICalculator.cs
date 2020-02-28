using System.Collections.Generic;

namespace PostfixCalculator.Domain
{
    /// <summary>
    /// Интерфейс для калькулятора
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Возвращает результат вычислениия строки выражения
        /// </summary>
        /// <param name="expression">строка выражения</param>
        /// <returns>результат вычислений</returns>
        double Calculate(string expression);
    }
}