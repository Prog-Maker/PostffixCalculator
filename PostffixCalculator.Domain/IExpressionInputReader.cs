namespace PostfixCalculator.Domain

{
    /// <summary>
    /// Интерфейс для считывателей выражений
    /// </summary>
    public interface IExpressionInputReader
    {
        string GetExpression();
    }
}