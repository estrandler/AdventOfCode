using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("2023.Tests")]

public class Five : BaseLevel, ILevel
{
    public string SolveA()
    {
        var input = GetInput();
        var seeds = GetSeeds(input[0]);

        var seedToSoil = GetMapFromKey("seed-to-soil", input);
        var soilToFertalizer = GetMapFromKey("soil-to-fertilizer", input);
        var fertilizerToWater = GetMapFromKey("fertilizer-to-water", input);
        var waterToLight = GetMapFromKey("water-to-light", input);
        var lightToTemperature = GetMapFromKey("light-to-temperature", input);
        var temperatureToHumidity = GetMapFromKey("temperature-to-humidity", input);
        var humidityToLocation = GetMapFromKey("humidity-to-location", input);

        var result = seeds
            .Select(s => seedToSoil.FirstOrDefault(m => m.ContainsKey(s))?.MapToNext(s) ?? s)
            .Select(s => soilToFertalizer.FirstOrDefault(m => m.ContainsKey(s))?.MapToNext(s) ?? s)
            .Select(s => fertilizerToWater.FirstOrDefault(m => m.ContainsKey(s))?.MapToNext(s) ?? s)
            .Select(s => waterToLight.FirstOrDefault(m => m.ContainsKey(s))?.MapToNext(s) ?? s)
            .Select(s => lightToTemperature.FirstOrDefault(m => m.ContainsKey(s))?.MapToNext(s) ?? s)
            .Select(s => temperatureToHumidity.FirstOrDefault(m => m.ContainsKey(s))?.MapToNext(s) ?? s)
            .Select(s => humidityToLocation.FirstOrDefault(m => m.ContainsKey(s))?.MapToNext(s) ?? s)
            .Min();

        return result.ToString();
    }


    public string SolveB()
    {
        var input = GetInput();

        var seeds = GetSeedsPartTwo(input[0]);

        var seedToSoil = GetMapFromKey("seed-to-soil", input);
        var soilToFertalizer = GetMapFromKey("soil-to-fertilizer", input);
        var fertilizerToWater = GetMapFromKey("fertilizer-to-water", input);
        var waterToLight = GetMapFromKey("water-to-light", input);
        var lightToTemperature = GetMapFromKey("light-to-temperature", input);
        var temperatureToHumidity = GetMapFromKey("temperature-to-humidity", input);
        var humidityToLocation = GetMapFromKey("humidity-to-location", input);

        var locations = humidityToLocation.OrderBy(m => m.DestinationRangeStart).ToArray();

        long result = 0;

        for (int i = 0; i < locations.Length; i++)
        {
            long current = 0;
            var location = locations[i];

            for (long j = 0; j < location.RangeLength; j++)
            {
                long start = location.DestinationRangeStart + j;
                current = humidityToLocation.FirstOrDefault(m => m.ContainsValue(location.DestinationRangeStart + j))?.MapToPrevious(current) ?? current;
                current = temperatureToHumidity.FirstOrDefault(m => m.ContainsValue(current))?.MapToPrevious(current) ?? current;
                current = lightToTemperature.FirstOrDefault(m => m.ContainsValue(current))?.MapToPrevious(current) ?? current;
                current = waterToLight.FirstOrDefault(m => m.ContainsValue(current))?.MapToPrevious(current) ?? current;
                current = fertilizerToWater.FirstOrDefault(m => m.ContainsValue(current))?.MapToPrevious(current) ?? current;
                current = soilToFertalizer.FirstOrDefault(m => m.ContainsValue(current))?.MapToPrevious(current) ?? current;
                current = seedToSoil.FirstOrDefault(m => m.ContainsValue(current))?.MapToPrevious(current) ?? current;

                if (seeds.Any(s => s.ContainsKey(current)))
                {
                    result = current;
                    break;
                }
            }
        }

        return result.ToString();
    }
    internal long[] GetSeeds(string firstLine) => firstLine.Split(":")[1].Trim().Split(" ").Select(s => Convert.ToInt64(s)).ToArray();
    internal Map[] GetSeedsPartTwo(string firstLine)
    {
        var maps = new List<Map>();
        var allNumbers = GetSeeds(firstLine);

        for (int i = 0; i < allNumbers.Length; i += 2)
        {
            var seed = allNumbers[i];
            var amountToTake = allNumbers[i + 1];

            maps.Add(new Map
            {
                SourceRangeStart = seed,
                RangeLength = amountToTake
            });
        }

        return maps.ToArray();
    }

    internal Map[] GetMapFromKey(string key, string[] input)
    {
        var maps = new List<Map>();
        var idx = Array.IndexOf(input, input.Single(line => line.StartsWith(key))) + 1;

        while (idx < input.Length && !string.IsNullOrEmpty(input[idx]))
        {
            var mapValues = input[idx].Split(" ").Select(s => Convert.ToInt64(s)).ToArray();
            maps.Add(new Map
            {
                DestinationRangeStart = mapValues[0],
                SourceRangeStart = mapValues[1],
                RangeLength = mapValues[2]
            });

            idx++;
        }

        return [.. maps];
    }
}

public class Map
{
    public long DestinationRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long RangeLength { get; set; }

    internal bool ContainsKey(long key) => key >= SourceRangeStart && SourceRangeStart + RangeLength >= key;
    internal bool ContainsValue(long key) => key >= DestinationRangeStart && DestinationRangeStart + RangeLength >= key;
    internal long MapToNext(long key) => key - SourceRangeStart + DestinationRangeStart;
    internal long MapToPrevious(long key) => key + SourceRangeStart - DestinationRangeStart;

}

