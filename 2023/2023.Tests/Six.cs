namespace _2023.Tests;

public class SixTests
{

    [TestCase(0, 7, 0)]
    [TestCase(1, 6, 6)]
    [TestCase(2, 5, 10)]
    [TestCase(3, 4, 12)]
    [TestCase(4, 3, 12)]
    [TestCase(5, 2, 10)]
    [TestCase(6, 1, 6)]
    [TestCase(7, 0, 0)]
    public void CalculateDistance(int speed, int time, int expectedDistance)
    {
        // Arrange
        // Act
        var result = Six.CalculateDistance(speed, time);

        // Assert
        result.Should().Be(expectedDistance);
    }

    [Test]
    public void NumberLongerThanRecord()
    {
        // Arrange
        var allDistances = new long[] { 0, 6, 10, 12, 12, 10, 6, 0 };
        var record = 9;

        // Act
        var result = Six.CountHigherThanRecord(record, allDistances);

        //Assert
        result.Should().Be(4);
    }

    [Test]
    public void GetCombinationsFromTime()
    {
        // Arrange
        var time = 7;

        // Act
        var result = Six.GetCombinationsFromTime(time);

        //Assert
        result.Should().BeEquivalentTo(new List<(int speed, int time)>()
        {
            (0, 7),
            (1, 6),
            (2, 5),
            (3, 4),
            (4, 3),
            (5, 2),
            (6, 1),
            (7, 0),
        });
    }
}