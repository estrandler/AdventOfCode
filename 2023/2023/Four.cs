using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("2023.Tests")]

public class Four : BaseLevel, ILevel
{
    public string SolveA()
    {
        var result = Parse(GetInput())
            .Select(line => GetNumberMatching(line.winningNumbers, line.myNumbers))
            .Select(CalculateScore)
            .Aggregate(0, (total, next) => next + total);


        return result.ToString();
    }

    public string SolveB()
    {
        var lines = Parse(GetInput())
            .Select(line => AddNumberOfCopies(line.winningNumbers, line.myNumbers))
            .ToList();

        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var matching = GetNumberMatching(line.winningNumbers, line.myNumbers);

            for (var x = 0; x < line.copies; x++)
            {
                for (var j = 1; j <= matching; j++)
                {
                    if (i + j < lines.Count)
                        lines[i + j] = (lines[i + j].winningNumbers, lines[i + j].myNumbers, lines[i + j].copies + 1);
                }
            }
        }

        var result = lines
            .Aggregate(0, (total, next) => next.copies + total);

        return result.ToString();
    }


    internal int GetNumberMatching(int[] winningNumbers, int[] myNumbers) => myNumbers.Where(n => winningNumbers.Contains(n)).Count();
    internal int CalculateScore(int numberMatching)
    {
        int result = 0;
        for (int i = 0; i < numberMatching; i++)
        {
            result = i == 0 ? 1 : result * 2;
        }

        return result;
    }

    internal List<(int[] winningNumbers, int[] myNumbers)> Parse(string[] lines)
    {
        return lines
            .Select(line => line.Split(":")[1])
            .Select(line => line.Split("|"))
            .Select(line =>
            {
                var winningNumbers = line[0].Trim().Replace("  ", " ").Split(" ").Select(val => Convert.ToInt32(val)).ToArray();
                var myNumbers = line[1].Trim().Replace("  ", " ").Split(" ").Select(val => Convert.ToInt32(val)).ToArray();

                return (winningNumbers, myNumbers);
            })
            .ToList();
    }

    internal (int[] winningNumbers, int[] myNumbers, int copies) AddNumberOfCopies(int[] winningNumbers, int[] myNumbers)
         => (winningNumbers, myNumbers, 1);
}
