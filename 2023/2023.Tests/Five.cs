namespace _2023.Tests;

public class FiveTests
{
    private string[] _lines;

    [SetUp]
    public void SetUp()
    {
        _lines =
        [
            "seeds: 79 14 55 13",
            "",
            "seed-to-soil map:",
            "50 98 2",
            "52 50 48",
            "",
            "soil-to-fertilizer map:",
            "0 15 37",
            "37 52 2",
            "39 0 15",
            "",
            "fertilizer-to-water map:",
            "49 53 8",
            "0 11 42",
            "42 0 7",
            "57 7 4",
            "",
            "water-to-light map:",
            "88 18 7",
            "18 25 70",
            "",
            "light-to-temperature map:",
            "45 77 23",
            "81 45 19",
            "68 64 13",
            "",
            "temperature-to-humidity map:",
            "0 69 1",
            "1 0 69",
            "",
            "humidity-to-location map:",
            "60 56 37",
            "56 93 4",
        ];
    }

    [Test]
    public void GetSeeds()
    {
        // Arrange
        var line = "seeds: 79 14 55 13";

        // Act
        var result = new Five().GetSeeds(line);

        // Assert
        result.Should().BeEquivalentTo(new[] { 79, 14, 55, 13 });
    }

    [Test]
    public void GetMapFromKey_seed_to_soil()
    {
        // Arrange
        var key = "seed-to-soil";

        // Act
        var result = new Five().GetMapFromKey(key, _lines);

        // Assert
        result.Should().BeEquivalentTo(new Map[] {
            new Map
            {
                DestinationRangeStart = 50,
                SourceRangeStart = 98,
                RangeLength = 2
            },
            new Map
            {
                DestinationRangeStart = 52,
                SourceRangeStart = 50,
                RangeLength = 48
            }
        });
    }

    [Test]
    public void GetMapFromKey_soil_to_fertilizer()
    {
        // Arrange
        var key = "soil-to-fertilizer";

        // Act
        var result = new Five().GetMapFromKey(key, _lines);

        // Assert
        result.Should().BeEquivalentTo(new Map[] {
            new Map
            {
                DestinationRangeStart = 0,
                SourceRangeStart = 15,
                RangeLength = 37
            },
            new Map
            {
                DestinationRangeStart = 37,
                SourceRangeStart = 52,
                RangeLength = 2
            },
            new Map
            {
                DestinationRangeStart = 39,
                SourceRangeStart = 0,
                RangeLength = 15
            }
        });
    }

    [Test]
    public void GetMapFromKey_humidity_to_location()
    {
        // Arrange
        var key = "humidity-to-location";

        // Act
        var result = new Five().GetMapFromKey(key, _lines);

        // Assert
        result.Should().BeEquivalentTo(new Map[] {
            new Map
            {
                DestinationRangeStart = 60,
                SourceRangeStart = 56,
                RangeLength = 37
            },
            new Map
            {
                DestinationRangeStart = 56,
                SourceRangeStart = 93,
                RangeLength = 4
            }
        });
    }

    [Test]
    public void ContainsValue_should_return_false_when_not_containing()
    {

        // Arrange
        var seed = 79;
        var map = new Map
        {
            DestinationRangeStart = 50,
            SourceRangeStart = 98,
            RangeLength = 2
        };

        // Act
        var result = map.ContainsValue(seed);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void ContainsValue_should_return_true_when_containing()
    {

        // Arrange
        var seed = 79;
        var map = new Map
        {
            DestinationRangeStart = 52,
            SourceRangeStart = 50,
            RangeLength = 48
        };

        // Act
        var result = map.ContainsValue(seed);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void The_whole_shebang_a()
    {

        // Arrange

        // Act
        var result = new Five().SolveA();

        // Assert
        result.Should().Be("35");
    }
}