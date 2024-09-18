namespace CamelTransport.Tests;

using Xunit;
using CamelTransport;

public class CamelTest
{
    [Fact(DisplayName = "non-positive capacity")]
    public void InvalidCapacity()
    {
        Assert.Throws<ArgumentException>(() => new Camel(-10));
        Assert.Throws<ArgumentException>(() => new Camel(0));

        Assert.Null(Record.Exception(() => new Camel(1)));
        Assert.Null(Record.Exception(() => new Camel(2)));
        Assert.Null(Record.Exception(() => new Camel(1000)));
        Assert.Null(Record.Exception(() => new Camel(1000000000)));
    }

    [Fact(DisplayName = "non-positive number of bananas")]
    public void InvalidNumberOfBananas()
    {
        var camel = new Camel(1000);

        Assert.Throws<ArgumentException>(() => camel.Transport(-800, 500));
        Assert.Throws<ArgumentException>(() => camel.Transport(0, 500));

        Assert.Null(Record.Exception(() => camel.Transport(1, 500)));
        Assert.Null(Record.Exception(() => camel.Transport(500, 500)));
        Assert.Null(Record.Exception(() => camel.Transport(2000, 500)));
    }

    [Fact(DisplayName = "non-positive miles")]
    public void InvalidMiles()
    {
        var camel = new Camel(1000);

        Assert.Throws<ArgumentException>(() => camel.Transport(800, -500));
        Assert.Throws<ArgumentException>(() => camel.Transport(800, 0));

        Assert.Null(Record.Exception(() => camel.Transport(800, 1)));
        Assert.Null(Record.Exception(() => camel.Transport(2000, 50)));
        Assert.Null(Record.Exception(() => camel.Transport(1, 1000000)));
    }

    [Fact(DisplayName = "1 trip")]
    public void NormalCase_1()
    {
        var camel = new Camel(1000);

        Assert.Equal(0, camel.Transport(1, 200));
        Assert.Equal(0, camel.Transport(150, 200));
        Assert.Equal(0, camel.Transport(199, 200));
        Assert.Equal(0, camel.Transport(200, 200));

        Assert.Equal(1, camel.Transport(201, 200));
        Assert.Equal(388, camel.Transport(588, 200));
        Assert.Equal(788, camel.Transport(988, 200));
        Assert.Equal(798, camel.Transport(998, 200));
        Assert.Equal(799, camel.Transport(999, 200));

        Assert.Equal(800, camel.Transport(1000, 200));

        Assert.Equal(800, camel.Transport(1001, 200));
        Assert.Equal(800, camel.Transport(1002, 200));
        Assert.Equal(800, camel.Transport(1012, 200));
        Assert.Equal(800, camel.Transport(1022, 200));
        Assert.Equal(800, camel.Transport(1122, 200));
        Assert.Equal(800, camel.Transport(1300, 200));
        Assert.Equal(800, camel.Transport(1400, 200));
    }

    [Fact(DisplayName = "2 trip 0 drop")]
    public void NormalCase_2_0()
    {
        var camel = new Camel(1000);

        Assert.Equal(801, camel.Transport(1401, 200));
        Assert.Equal(802, camel.Transport(1402, 200));
        Assert.Equal(810, camel.Transport(1410, 200));
        Assert.Equal(820, camel.Transport(1420, 200));
        Assert.Equal(1020, camel.Transport(1620, 200));
        Assert.Equal(1220, camel.Transport(1820, 200));
        Assert.Equal(1398, camel.Transport(1998, 200));
        Assert.Equal(1399, camel.Transport(1999, 200));

        Assert.Equal(1400, camel.Transport(2000, 200));

        Assert.Equal(1400, camel.Transport(2001, 200));
        Assert.Equal(1400, camel.Transport(2200, 200));
        Assert.Equal(1400, camel.Transport(2399, 200));
        Assert.Equal(1400, camel.Transport(2400, 200));
    }

    [Fact(DisplayName = "3 trip 0 drop")]
    public void NormalCase_3_0()
    {
        var camel = new Camel(1000);

        Assert.Equal(1401, camel.Transport(2401, 200));
        Assert.Equal(1402, camel.Transport(2402, 200));
        Assert.Equal(1412, camel.Transport(2412, 200));
        Assert.Equal(1712, camel.Transport(2712, 200));
        Assert.Equal(1999, camel.Transport(2999, 200));

        Assert.Equal(2000, camel.Transport(3000, 200));

        Assert.Equal(2000, camel.Transport(3001, 200));
        Assert.Equal(2000, camel.Transport(3283, 200));
        Assert.Equal(2000, camel.Transport(3284, 200));
    }

    [Fact(DisplayName = "4 trip 0 drop")]
    public void NormalCase_4_0()
    {
        var camel = new Camel(1000);

        Assert.Equal(2001, camel.Transport(3285, 200));
        Assert.Equal(2002, camel.Transport(3286, 200));
        Assert.Equal(2003, camel.Transport(3287, 200));
        Assert.Equal(2013, camel.Transport(3297, 200));
        Assert.Equal(2023, camel.Transport(3307, 200));
        Assert.Equal(2123, camel.Transport(3407, 200));
        Assert.Equal(2223, camel.Transport(3507, 200));
        Assert.Equal(2423, camel.Transport(3707, 200));
        Assert.Equal(2623, camel.Transport(3907, 200));
        Assert.Equal(2710, camel.Transport(3999, 200));

        Assert.Equal(2710, camel.Transport(4000, 200));

        Assert.Equal(2710, camel.Transport(4001, 200));
        Assert.Equal(2710, camel.Transport(4002, 200));
        Assert.Equal(2710, camel.Transport(4012, 200));
        Assert.Equal(2710, camel.Transport(4112, 200));
        Assert.Equal(2710, camel.Transport(4222, 200));
    }

    [Fact(DisplayName = "2 trip 1 drop")]
    public void NormalCase_2_1()
    {
        var camel = new Camel(1000);

        Assert.Equal(331, camel.Transport(1997, 1000));
        Assert.Equal(332, camel.Transport(1998, 1000));
        Assert.Equal(333, camel.Transport(1999, 1000));

        Assert.Equal(333, camel.Transport(2000, 1000));

        Assert.Equal(333, camel.Transport(2001, 1000));
        Assert.Equal(333, camel.Transport(2067, 1000));
    }

    [Fact(DisplayName = "3 trip 2 drop")]
    public void NormalCase_3_2()
    {
        var camel = new Camel(1000);

        Assert.Equal(531, camel.Transport(2997, 1000));
        Assert.Equal(532, camel.Transport(2998, 1000));
        Assert.Equal(533, camel.Transport(2999, 1000));

        Assert.Equal(533, camel.Transport(3000, 1000));

        Assert.Equal(533, camel.Transport(3001, 1000));
        Assert.Equal(533, camel.Transport(3100, 1000));
    }

    [Fact(DisplayName = "4 trip 3 drop")]
    public void NormalCase_4_3()
    {
        var camel = new Camel(1000);

        Assert.Equal(675, camel.Transport(3996, 1000));
        Assert.Equal(675, camel.Transport(3997, 1000));
        Assert.Equal(675, camel.Transport(3998, 1000));
        Assert.Equal(675, camel.Transport(3999, 1000));

        Assert.Equal(675, camel.Transport(4000, 1000));

        Assert.Equal(675, camel.Transport(4001, 1000));
        Assert.Equal(675, camel.Transport(4201, 1000));
    }

    [Fact(DisplayName = "5 trip 4 drop")]
    public void NormalCase_5_4()
    {
        var camel = new Camel(1000);

        Assert.Equal(786, camel.Transport(4998, 1000));
        Assert.Equal(786, camel.Transport(4999, 1000));

        Assert.Equal(786, camel.Transport(5000, 1000));

        Assert.Equal(786, camel.Transport(5001, 1000));
        Assert.Equal(786, camel.Transport(5002, 1000));
        Assert.Equal(786, camel.Transport(5102, 1000));
    }
}