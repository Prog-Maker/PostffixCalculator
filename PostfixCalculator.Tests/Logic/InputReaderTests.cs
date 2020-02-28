namespace CalculatorSolution.Test.Logic
{
    using Moq;
    using NUnit.Framework;
    using PostfixCalculator.Domain;

    [TestFixture]
    public class InputReaderTests
    {
        [Test]
        public void Reader_ReturnsCorrectExpression()
        {
            var moqReader = new Mock<IExpressionInputReader>();
            moqReader.Setup(moq => moq.GetExpression()).Returns("2+3*5");
            var result = moqReader.Object.GetExpression();
            Assert.AreEqual("2+3*5",result);
        }
    }
}