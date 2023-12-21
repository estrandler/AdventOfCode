
namespace _2023.Tests;

public class NineTests
{

    [Test]
    public void Parse()
    {
        // Arrange
        var input = new[] {
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45",
        };

        // Act
        var lines = Nine.Parse(input);

        //Assert
        lines.Should().BeEquivalentTo(new[] {
            new int[] {
                0, 3, 6, 9 , 12, 15
            },
            [
                1, 3, 6, 10 , 15, 21
            ],
            [
                10, 13, 16, 21 , 30, 45
            ]
        });
    }

    [Test]
    public void CalculateDifferences()
    {
        // Arrange
        var input = new[] {
            new int[] {
                0, 3, 6, 9 , 12, 15
            },
            [
                1, 3, 6, 10 , 15, 21
            ],
            [
                10, 13, 16, 21 , 30, 45
            ]
        };

        // Act
        var result = Nine.CalculateDifferences(input[0]);
        var result1 = Nine.CalculateDifferences(input[1]);
        var result2 = Nine.CalculateDifferences(input[2]);

        //Assert
        result.Should().BeEquivalentTo(new[] { 3, 3, 3, 3, 3 });
        result1.Should().BeEquivalentTo(new[] { 2, 3, 4, 5, 6 });
        result2.Should().BeEquivalentTo(new[] { 3, 3, 5, 9, 15 });
    }

    [Test]
    public void Should_stop_when_all_0()
    {
        // Arrange
        var input = new[] {
            new int[] {
                0, 3, 6, 9 , 12, 15
            },
            [
                1, 3, 6, 10 , 15, 21
            ],
            [
                10, 13, 16, 21 , 30, 45
            ]
        };

        // Act
        var result = Nine.LoopUntilAllZero(input[0]);
        var result1 = Nine.LoopUntilAllZero(input[1]);
        var result2 = Nine.LoopUntilAllZero(input[2]);

        //Assert
        result.Should().BeEquivalentTo(new[] {
            new int[] {
                0, 3, 6, 9 , 12, 15
            },
            [
                3, 3, 3, 3 , 3
            ],
        });
        result1.Should().BeEquivalentTo(new[] {
            new int[] {
                1, 3, 6, 10 , 15, 21
            },
            [
                2, 3, 4, 5, 6
            ],
            [
                1, 1, 1, 1
            ],
        });
        result2.Should().BeEquivalentTo(new[] {
            new int[] {
                10, 13, 16, 21 , 30, 45
            },
            [
                3, 3, 5, 9 , 15,
            ],
            [
                0, 2, 4, 6 , 
            ],
            [
                2, 2, 2, 
            ],
        });
    }

}