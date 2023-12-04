namespace _2023.Tests;

public class FourTests
{
    [TestCase(new[] { 41, 48, 83, 86, 17 }, new[] { 83, 86, 6, 31, 17, 9, 48, 53 }, 4)]
    [TestCase(new[] { 13, 32, 20, 16, 61 }, new[] { 61, 30, 68, 82, 17, 32, 24, 19 }, 2)]
    [TestCase(new[] { 1, 21, 53, 59, 44 }, new[] { 69, 82, 63, 72, 16, 21, 14, 1 }, 2)]
    [TestCase(new[] { 41, 92, 73, 84, 69 }, new[] { 59, 84, 76, 51, 58, 5, 54, 83 }, 1)]
    [TestCase(new[] { 87, 83, 26, 28, 32, }, new[] { 88, 30, 70, 12, 93, 22, 82, 36 }, 0)]
    [TestCase(new[] { 31, 18, 13, 5, 6, 72, }, new[] { 74, 77, 10, 23, 35, 67, 36, 11 }, 0)]
    public void GetNumberMatching_should_match(int[] winningNumbers, int[] myNumbers, int expectedResult)
    {
        // Arrange
        // Act
        var result = new Four().GetNumberMatching(winningNumbers, myNumbers);


        // Assert
        result.Should().Be(expectedResult);

    }

    [TestCase(4, 8)]
    [TestCase(2, 2)]
    [TestCase(1, 1)]
    [TestCase(0, 0)]
    public void CalculateScore(int numberMatching, int score)
    {
        // Arrange
        // Act
        var result = new Four().CalculateScore(numberMatching);


        // Assert
        result.Should().Be(score);

    }

    [Test]
    public void Parse()
    {
        // Arrange
        var lines = new[]
        {
            "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11",
        };

        // Act
        var result = new Four().Parse(lines);

        // Assert
        result.Should().BeEquivalentTo(new List<(int[], int[])>()
        {
            (new[] { 41, 48, 83, 86, 17 }, new[] { 83, 86, 6, 31, 17, 9, 48, 53 }),
            (new[] { 13, 32, 20, 16, 61 }, new[] { 61, 30, 68, 82, 17, 32, 24, 19 }),
            (new[] { 1, 21, 53, 59, 44 }, new[] { 69, 82, 63, 72, 16, 21, 14, 1 }),
            (new[] { 41, 92, 73, 84, 69 }, new[] { 59, 84, 76, 51, 58, 5, 54, 83 }),
            (new[] { 87, 83, 26, 28, 32, }, new[] { 88, 30, 70, 12, 93, 22, 82, 36 }),
            (new[] { 31, 18, 13, 56, 72, }, new[] { 74, 77, 10, 23, 35, 67, 36, 11 }),
        });
    }

}