using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("2023.Tests")]

public class Three : BaseLevel, ILevel
{
    public string SolveA()
    {
        var input = GetInput();

        var allSymbols = Array.Empty<(int line, int index)>();
        var allNumbers = Array.Empty<(int line, int index, int end, int number)>();

        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var symbols = GetSymbolsFromLine(i, line);
            var numbers = GetNumbers(i, line);

            allSymbols = allSymbols.Concat(symbols).ToArray();
            allNumbers = allNumbers.Concat(numbers).ToArray();
        }

        var result = allNumbers
            .Where(number => allSymbols.Any(symbol => IsNearby(symbol, number)))
            .Aggregate(0, (total, next) => next.number + total);

        return result.ToString();
    }

    public string SolveB()
    {
        var input = GetInput();

        var allSymbols = Array.Empty<(int line, int index)>();
        var allNumbers = Array.Empty<(int line, int index, int end, int number)>();
        var allGears = Array.Empty<(int line, int index)>();

        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var symbols = GetSymbolsFromLine(i, line);
            var numbers = GetNumbers(i, line);
            var gears = GetGears(i, line);

            allSymbols = allSymbols.Concat(symbols).ToArray();
            allNumbers = allNumbers.Concat(numbers).ToArray();
            allGears = allGears.Concat(gears).ToArray();
        }

        var result = allGears
            .Select(gear =>
            {
                var matching = allNumbers.Where(num => IsNearby(gear, num));

                return new
                {
                    isValid = matching.Count() == 2,
                    gearRatio = matching.Aggregate(0, (total, next) => total == 0 ? next.number : total * next.number)
                };
            })
            .Where(g => g.isValid)
            .Aggregate(0, (total, next) => next.gearRatio + total);

        return result.ToString();
    }

    internal bool IsNearby((int line, int index) symbol, (int line, int index, int end, int number) number)
    {
        return symbol.line <= number.line + 1 &&
              symbol.line >= number.line - 1 &&
              symbol.index >= number.index - 1 &&
              symbol.index <= number.end + 1;
    }

    internal (int line, int index)[] GetSymbolsFromLine(int lineNumber, string line) =>
        Regex.Matches(line, @"(?s)[^0-9|.]")
            .Select(match => (lineNumber, match.Index)).ToArray();

    internal (int line, int index, int end, int number)[] GetNumbers(int y, string line) =>
        Regex.Matches(line, @"[0-9]+")
            .Select(match => (y, match.Index, match.Index + match.Length - 1, Convert.ToInt32(match.Value))).ToArray();

    internal (int line, int index)[] GetGears(int lineNumber, string line) =>
        Regex.Matches(line, @"\*")
            .Select(match => (lineNumber, match.Index)).ToArray();
}
