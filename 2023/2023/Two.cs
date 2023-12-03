using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("2023.Tests")]

public class Two : BaseLevel, ILevel
{
    public string SolveA()
    {
        var result = GetInput()
            .Select(line =>
            {
                var l = line.Split(":");
                return new Row
                {
                    Id = Convert.ToInt32(l[0].Split(" ")[1]),
                    Games = l[1].Split(";").Select(g => new Game
                    {
                        Sets = g
                            .Split(",")
                            .Select(w => w.Trim())
                            .ToDictionary(k => k.Split(" ")[1], s => Convert.ToInt32(s.Split(" ")[0]))
                    }).ToArray(),
                    Raw = line
                };
            })
            .Where(g => g.IsValid())
            .Aggregate(0, (total, next) => next.Id + total);
            

        return result.ToString();
    }

    public string SolveB()
    {
        var result = GetInput()
            .Select(line =>
            {
                var l = line.Split(":");
                return new Row
                {
                    Id = Convert.ToInt32(l[0].Split(" ")[1]),
                    Games = l[1].Split(";").Select(g => new Game
                    {
                        Sets = g
                            .Split(",")
                            .Select(w => w.Trim())
                            .ToDictionary(k => k.Split(" ")[1], s => Convert.ToInt32(s.Split(" ")[0]))
                    }).ToArray(),
                    Raw = line
                };
            })
            .Select(g => g.CalculatePower())
            .Aggregate(0, (total, next) => next + total);
        
        return result.ToString();
    }
}

internal class Row
{
    public int Id { get; set; }
    public string Raw { get; set; }
    public Game[] Games { get; set; }

    public bool IsValid() => Games.All(g => g.IsValid());
    

    public int CalculatePower()
    {
        var blue = Games.Where(g => g.Sets.ContainsKey("blue")).Select(g => g.Sets["blue"]).Max();
        var red = Games.Where(g => g.Sets.ContainsKey("red")).Select(g => g.Sets["red"]).Max();
        var green = Games.Where(g => g.Sets.ContainsKey("green")).Select(g => g.Sets["green"]).Max();

        return blue * red * green;
    }
}

internal class Game
{
    private readonly Dictionary<string, int> _max = new()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };
    public Dictionary<string, int> Sets { get; set; }
    public bool IsValid() => Sets.All(kvp => kvp.Value <= _max[kvp.Key]);
}