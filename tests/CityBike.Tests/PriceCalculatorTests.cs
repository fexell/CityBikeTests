using CityBike.Core;
using Xunit;
 
namespace CityBike.Tests;
 
public class PriceCalculatorTests
{
    private readonly PriceCalculator _calculator = new();
 
    [Fact]
    public void Calculate_18Minutes_Returns55()
    {
        decimal price = _calculator.Calculate(18 * 60);
 
        Assert.Equal(55m, price);
    }

    [Fact]
    public void Calculate_0Seconds_ReturnsUnlockFee10()
    {
        decimal price = _calculator.Calculate(0);

        Assert.Equal(10m, price);
    }

    [Fact]
    public void Calculate_100Minutes_ReturnsMaxPrice250()
    {
        decimal price = _calculator.Calculate(100 * 60);

        Assert.Equal(250m, price);
    }

    [Fact]
    public void Calculate_61Seconds_Returns15()
    {
        decimal price = _calculator.Calculate(61);

        Assert.Equal(15m, price);
    }

    [Fact]
    public void Calculate_NegativeSeconds_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(-1));
    }
}
