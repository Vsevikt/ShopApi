using MyCalculator;

namespace ShopTest
{
    public class CalculatorTest
    {
        [Fact]
        public void Sum_ReturnsCorrectResult()
        {
            int a = 10, b = 20;
            int result = Calculator.Min(a, b);
            Assert.Equal(-10, result);
        }
    }
}