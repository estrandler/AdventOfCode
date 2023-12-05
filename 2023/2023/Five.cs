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
            .Select(s => seedToSoil.FirstOrDefault(m => m.ContainsValue(s))?.MapToNext(s) ?? s)
            .Select(s => soilToFertalizer.FirstOrDefault(m => m.ContainsValue(s))?.MapToNext(s) ?? s)
            .Select(s => fertilizerToWater.FirstOrDefault(m => m.ContainsValue(s))?.MapToNext(s) ?? s)
            .Select(s => waterToLight.FirstOrDefault(m => m.ContainsValue(s))?.MapToNext(s) ?? s)
            .Select(s => lightToTemperature.FirstOrDefault(m => m.ContainsValue(s))?.MapToNext(s) ?? s)
            .Select(s => temperatureToHumidity.FirstOrDefault(m => m.ContainsValue(s))?.MapToNext(s) ?? s)
            .Select(s => humidityToLocation.FirstOrDefault(m => m.ContainsValue(s))?.MapToNext(s) ?? s)
            .Min();

        return result.ToString();
    }


    public string SolveB()
    {

        return string.Empty;
    }
    internal long[] GetSeeds(string firstLine) => firstLine.Split(":")[1].Trim().Split(" ").Select(s => Convert.ToInt64(s)).ToArray();

    internal Map[] GetMapFromKey(string key, string[] input)
    {
        var maps = new List<Map>();
        var idx = Array.IndexOf(input, input.Single(line => line.StartsWith(key))) + 1;

        while (idx < input.Length && !string.IsNullOrEmpty(input[idx]))
        {
            var mapValues = input[idx].Split(" ").Select(s => Convert.ToInt64(s)).ToArray();
            var map = new Map
            {
                DestinationRangeStart = mapValues[0],
                SourceRangeStart = mapValues[1],
                RangeLength = mapValues[2]
            };
            maps.Add(map);

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

    internal bool ContainsValue(long key) => key >= SourceRangeStart && SourceRangeStart + RangeLength >= key;
    internal long MapToNext(long key) => key - SourceRangeStart + DestinationRangeStart;
}

