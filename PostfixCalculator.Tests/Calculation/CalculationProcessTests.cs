namespace CalculatorSolution.Test.Calculation
{
    using System;
    using NUnit.Framework;
    using global::PostfixCalculator.Application;
    using System.Collections.Generic;

    [TestFixture]
    public class CalculationProcessTests
    {
        private PostfixCalculator calculator;
        private PostfixCalculatorCreator creator;

        [SetUp]
        public void CalculationProcessTestsSetUp()
        {
            creator = new PostfixCalculatorCreator(new BaseRecognizer(), WayToGetOperation.Internal);

            calculator = creator.CreateCalculator();
        }

        [Test]
        public void Calculator_CalculatesSimpleExpressionProperly()
        {
            var result = calculator.Calculate("2+2*2");
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Calculator_CalculatesSimpleExpressionProperlyWhithParentheses()
        {
            var result = calculator.Calculate("(2+2)*2");
            Assert.AreEqual(8, result);
        }

        [Test]
        public void Calculator_CalculatesSimpleSQRTProperly()
        {
            var result = calculator.Calculate("sqrt4");
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Calculator_CalculatesSimpleSQRTProperlyWhithParentheses()
        {
            var result = calculator.Calculate("(sqrt9)*2");
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Calculator_CalculatesSimpleExpressionWithFloatingPoinNumbersProperly()
        {
            var result = calculator.Calculate("2.5+2.5");
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Calculator_CalculatesSimpleExpressionWithFloatingPoinNumbersProperlyWhithParentheses()
        {
            var result = calculator.Calculate("2*(2.5+2.5)");
            Assert.AreEqual(10, result);
        }

        [Test]
        public void Calculator_CalculatesExpressionWithMultipleOperationsProperly()
        {
            var result = calculator.Calculate("2+6*10-2");
            Assert.AreEqual(60, result);
        }

        [Test]
        public void Calculator_CalculatesExpressionWithMultipleOperationsAndFloatingPointNumbersProperly()
        {
            var result = calculator.Calculate("2.1+6.3*5.5-2.5");
            Assert.AreEqual(34.25, result);
        }

        [Test]
        public void Calculator_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => calculator.Calculate("2//2"));
        }

        [Test]
        public void Calculator_ReordersOperandsInCorrectOrder()
        {
            var result = calculator.ReorderInPostfixNotation(new List<string> 
                                                                 { "2", "+", "6", "*", "10", "-", "2" });
            Assert.AreEqual(new[] { "2", "6", "10", "*", "2", "-", "+" }, result);
        }

        [Test]
        public void Calculator_ReordersOperandsInCorrectOrderWithParentheses()
        {
            var result = calculator.ReorderInPostfixNotation(new List<string> 
                       { "2", "+", "6", "*", "10", "-", "2", "/", "(", "2", "*", "4", ")" });
            Assert.AreEqual(new[] { "2", "6", "10", "*", "2", "2", "4", "*", "/", "-", "+" }, 
                                                                                     result);
        }
    }
}