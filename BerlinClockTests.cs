using System.Runtime.InteropServices.JavaScript;

namespace BerlinClockCodeKata;

public class Tests
{
    [TestCase("00:00:00", "OOOO")]
    [TestCase("23:59:59", "YYYY")]
    [TestCase("12:32:00", "YYOO")]
    [TestCase("12:34:00", "YYYY")]
    [TestCase("12:35:00", "OOOO")]
    public void BerlinClockReturnsCorrectSingleMinutes(string time, string expected)
    {
        var clock = new BerlinClock();
        var actual = clock.GetSingleMinutes(time);
        Assert.AreEqual(expected, actual);
    }
    
    [TestCase("00:00:00", "OOOO")]
    [TestCase("23:59:59", "RRRO")]
    [TestCase("02:04:00", "RROO")]
    [TestCase("08:23:00", "RRRO")]
    [TestCase("14:35:00", "RRRR")]
    public void BerlinClockReturnsCorrectSingleHours(string time, string expected)
    {
        var clock = new BerlinClock();
        var actual = clock.GetSingleHours(time);
        Assert.AreEqual(expected, actual);
    }
    
    [TestCase("00:00:00", "OOOOOOOOOOO")]
    [TestCase("23:59:59", "YYRYYRYYRYY")]
    [TestCase("12:37:00", "YYRYYRYOOOO")]
    [TestCase("12:04:00", "OOOOOOOOOOO")]
    [TestCase("12:23:00", "YYRYOOOOOOO")]
    [TestCase("12:35:00", "YYRYYRYOOOO")]
    public void BerlinClockReturnsCorrectFiveMinutes(string time, string expected)
    {
        var clock = new BerlinClock();
        var actual = clock.GetFiveMinutes(time);
        Assert.AreEqual(expected, actual);
    }
}

public class BerlinClock
{
    public Row SingleMinutesRow { get; } = new Row(4);

    public Row SingleHoursRow { get; } = new Row(4);
    
    public Row FiveMinutesRow {get; } = new Row(11);


    public string GetSingleMinutes(string time)
    {
        var minutes = time.Split(":")[1];
        var numberOfLampsOn = int.Parse(minutes) % 5;
        SingleMinutesRow.SetLampsOn(numberOfLampsOn);
        return new String(SingleMinutesRow.Lamps.Select(l => (char)l.Colour).ToArray());
    }

    public string GetSingleHours(string time)
    {
        var hours = time.Split(":")[0];
        var numberOfLampsOn = int.Parse(hours) % 5;
        SingleHoursRow.SetLampsOn(numberOfLampsOn, LampColour.Red);
        return new String(SingleHoursRow.Lamps.Select(l => (char)l.Colour).ToArray());
    }

    public string GetFiveMinutes(string time)
    {
        var lampColours = new []
        {
            LampColour.Yellow,
            LampColour.Yellow,
            LampColour.Red,
            LampColour.Yellow,
            LampColour.Yellow,
            LampColour.Red,
            LampColour.Yellow,
            LampColour.Yellow,
            LampColour.Red,
            LampColour.Yellow,
            LampColour.Yellow
        };
        
        var minutes = time.Split(":")[1];
        var numberOfLampsOn = int.Parse(minutes) / 5;
        FiveMinutesRow.SetLampsOn(numberOfLampsOn, lampColours);
        return new String(FiveMinutesRow.Lamps.Select(l => (char)l.Colour).ToArray());
    }
}

public class Row
{
    public Row(int numberOfLamps)
    {
        Lamps = new List<Lamp>();
        for (var i = 0; i < numberOfLamps; i++)
        {
            Lamps.Add(new Lamp { Colour = LampColour.Off });
        }
    }
    
    
    public List<Lamp> Lamps { get; set; }
    
    public void SetLampsOn(int numberOfLamps, LampColour lampColour = LampColour.Yellow)
    {
        for (var i = 0; i < numberOfLamps; i++)
        {
            Lamps[i].Colour = lampColour;
        }
    }
    
    public void SetLampsOn(int numberOfLamps, LampColour[] lampColours)
    {
        for (var i = 0; i < numberOfLamps; i++)
        {
            Lamps[i].Colour = lampColours[i];
        }
    }
}

public class Lamp
{
    public LampColour Colour { get; set; }
}

public enum LampColour
{
    Red = 'R',
    Yellow = 'Y',
    Off = 'O'
}