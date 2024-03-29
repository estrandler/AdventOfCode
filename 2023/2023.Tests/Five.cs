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
    public void ContainsKey_should_return_false_when_not_containing()
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
        var result = map.ContainsKey(seed);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void ContainsKey_should_return_true_when_containing()
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
        var result = map.ContainsKey(seed);

        // Assert
        result.Should().BeTrue();
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

    //[Test]
    //public void GetSeedsPartTwo()
    //{

    //    // Arrange
    //    var line = "seeds: 79 14 55 13";

    //    // Act
    //    var result = new Five().GetSeedsPartTwo(line);

    //    // Assert
    //    result.Should().BeEquivalentTo(new[] { 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67 });
    //}



    [Test]
    public void ContainsValue_should_return_false_when_not_containing()
    {

        // Arrange
        var location = 79;
        var map = new Map
        {
            DestinationRangeStart = 50,
            SourceRangeStart = 98,
            RangeLength = 2
        };

        // Act
        var result = map.ContainsValue(location);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void ContainsValue_should_return_true_when_containing()
    {

        // Arrange
        var location = 79;
        var map = new Map
        {
            DestinationRangeStart = 52,
            SourceRangeStart = 50,
            RangeLength = 48
        };

        // Act
        var result = map.ContainsValue(location);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void MapToPrevious_should_return_correct_mapping()
    {

        // Arrange
        var location = 79;
        var map = new Map
        {
            DestinationRangeStart = 52,
            SourceRangeStart = 50,
            RangeLength = 48
        };

        // Act
        var result = map.MapToPrevious(location);

        // Assert
        result.Should().Be(77);
    }

    [Test]
    public void ContainsValue_false_MapToPrevious_should_fallback_to_same_number()
    {

        // Arrange
        // Arrange
        var location = 79;
        var maps = new[]{ new Map
        {
            DestinationRangeStart = 50,
            SourceRangeStart = 98,
            RangeLength = 2
        }};

        // Act

        var map = maps.FirstOrDefault(m => m.ContainsValue(location));
        var result = map?.MapToPrevious(location) ?? location;

        // Assert
        map.Should().BeNull();
        result.Should().Be(79);
    }

    [Test]
    public void The_whole_shebang_b()
    {

        // Arrange

        // Act
        var result = new Five().SolveB();

        // Assert
        result.Should().Be("46");
    }
}