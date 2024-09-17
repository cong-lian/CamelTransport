namespace CamelTransport.Tests;

using Xunit;
using CamelTransport;
public class CamelTransportTest
{
    [Fact(DisplayName = "non-positive capacity")]
    public void InvalidCapacity()
    {
        Assert.Throws<ArgumentException>(() => new Camel(-10));
    }

    [Fact(DisplayName = "non-positive number of bananas")]
    public void InvalidNumberOfBananas()
    {
        var camel = new Camel(1000);
        Assert.Throws<ArgumentException>(() => camel.Transport(-800, 500));
    }

    [Fact(DisplayName = "non-positive miles")]
    public void InvalidMiles()
    {
        var camel = new Camel(1000);
        Assert.Throws<ArgumentException>(() => camel.Transport(800, -500));
    }

    [Fact(DisplayName = "one run")]
    public void Normal_Case_1()
    {
        var camel = new Camel(1000);
        Assert.Equal(300, camel.Transport(800, 500));
    }

    [Fact(DisplayName = "two run nonstop")]
    public void Normal_Case_2_0()
    {
        var camel = new Camel(1000);
        Assert.Equal(1400, camel.Transport(2000, 200));
    }

    [Fact(DisplayName = "three run nonstop")]
    public void Normal_Case_3_0()
    {
        var camel = new Camel(1000);
        Assert.Equal(2000, camel.Transport(3000, 200));
    }

    [Fact(DisplayName = "two run with one stop")]
    public void Normal_Case_2_1()
    {
        var camel = new Camel(1000);
        Assert.Equal(333, camel.Transport(2000, 1000));
    }

    [Fact(DisplayName = "three run with two stop")]
    public void Normal_Case_3_2()
    {
        var camel = new Camel(1000);
        Assert.Equal(533, camel.Transport(3000, 1000));
    }

    [Fact(DisplayName = "four run with three stop")]
    public void Normal_Case_4_3()
    {
        var camel = new Camel(1000);
        Assert.Equal(675, camel.Transport(4000, 1000));
    }

    [Fact(DisplayName = "five run with four stop")]
    public void Normal_Case_5_4()
    {
        var camel = new Camel(1000);
        Assert.Equal(786, camel.Transport(5000, 1000));
    }
}