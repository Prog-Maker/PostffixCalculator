namespace CalculatorSolution.Test.Logic
{
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using PostfixCalculator.Application;
    using PostfixCalculator.Domain;
    
    [TestFixture]
    public class RecognizerTests
    {
        [Test]
        public void GetFullNumber_ReturnsFullNumberFromExpression()
        {
            IRecognizer recognizer = new BaseRecognizer();
            var result = recognizer.GetFullNumber("7904fss");
            Assert.AreEqual("7904",result);
        }

        [Test]
        public void GetFullNumber_ReturnsFullNumberWithFloatingPointFromExpression()
        {
            IRecognizer recognizer = new BaseRecognizer();
            var result = recognizer.GetFullNumber("56.57rhrdhr");
            Assert.AreEqual("56.57",result);
        }

        [Test]
        public void GetFullOperation_ReturnsRecognizedOperation()
        {
            var moqOperation = new Mock<IOperation>();
            moqOperation.Setup(operation => operation.StringPresentation).Returns("+");
            var operationsList = new List<IOperation> {moqOperation.Object};
            IRecognizer recognizer = new BaseRecognizer(operationsList);

            var result = recognizer.GetFullOperation("+56");

            Assert.AreEqual("+",result);
        }

        [Test]
        public void GetFullOperation_ThrowsUnrecognizedOperationException()
        {
            var moqOperation = new Mock<IOperation>();
            moqOperation.Setup(operation => operation.StringPresentation).Returns("+");
            var operationsList = new List<IOperation> {moqOperation.Object};
            IRecognizer recognizer = new BaseRecognizer().CraeteOperations(operationsList);

            Assert.Throws<UnrecognizedOperationException>(() => recognizer.GetFullOperation("-23"));
        }

        [Test]
        public void Recognize_ReturnsCorrectNumberOfOperands()
        {
            var moqFirstOperation = new Mock<IOperation>();
            moqFirstOperation.Setup(operation => operation.StringPresentation).Returns("+");
            var moqSecondOperation = new Mock<IOperation>();
            moqSecondOperation.Setup(operation => operation.StringPresentation).Returns("-");
            var operationsList = new List<IOperation> {moqFirstOperation.Object, moqSecondOperation.Object};
            IRecognizer recognizer = new BaseRecognizer(operationsList);

            var result = recognizer.Recognize("2+6-8");

            Assert.AreEqual(5,result.Count);
        }

        [Test]
        public void Recognize_ReturnsCorrectRecognizedOperands()
        {
            var moqFirstOperation = new Mock<IOperation>();
            moqFirstOperation.Setup(operation => operation.StringPresentation).Returns("+");
            var moqSecondOperation = new Mock<IOperation>();
            moqSecondOperation.Setup(operation => operation.StringPresentation).Returns("-");
            var operationsList = new List<IOperation> { moqFirstOperation.Object, moqSecondOperation.Object };
            IRecognizer recognizer = new BaseRecognizer(operationsList);

            var result = recognizer.Recognize("2+6-8");

            Assert.AreEqual(new[] { "2", "+", "6", "-", "8" }, result);
        }
    }
}