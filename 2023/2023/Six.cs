using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("2023.Tests")]

public class Six : BaseLevel, ILevel
{

    private readonly List<(long time, long record)> _inputA =
    [
        (49, 263),
        (97, 1532),
        (94, 1378),
        (94, 1851)
    ];

    private readonly (long time, long record) _inputB = (49979494, 263153213781851);


    public string SolveA()
    {
        var result = _inputA
            .Select(r => new
            {
                Combinations = GetCombinationsFromTime(r.time),
                Record = r.record
            })
            .Select(r => new
            {
                Distances = r.Combinations.Select(c => CalculateDistance(c.speed, c.time)).ToArray(),
                r.Record
            })
            .Select(r => CountHigherThanRecord(r.Record, r.Distances))
            .Aggregate((long)1, (total, next) => next * total);

        return result.ToString();
    }


    public string SolveB()
    {

        var combinations = GetCombinationsFromTime(_inputB.time);
        var distances = combinations.Select(c => CalculateDistance(c.speed, c.time)).ToArray();
        var result = CountHigherThanRecord(_inputB.record, distances);

        return result.ToString();
    }
    internal static long CalculateDistance(long speed, long time) => speed * time;

    internal static long CountHigherThanRecord(long record, long[] allDistances) => allDistances.Count(x => x > record);

    internal static List<(long time, long speed)> GetCombinationsFromTime(long time)
    {
        var combinations = new List<(long speed, long time)>();
        for (var i = 0; i <= time; i++)
        {
            combinations.Add((i, time - i));
        }

        return combinations;
    }
}


