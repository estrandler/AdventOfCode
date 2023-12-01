using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("2023.Tests")]
public class One : BaseLevel, ILevel
{
    public string SolveA()
    {
        var result = GetInput()
            .Select(GetIntegersFromString)
            .Select(GetFirstAndLastNumeric)
            .Select(ConvertToNumber)
            .Aggregate(0, (total, next) => next + total);

        return result.ToString();
    }
    public string SolveB()
    {
        var result = GetInput()
            .Select(StringNumberToInteger)
            .Select(GetIntegersFromString)
            .Select(GetFirstAndLastNumeric)
            .Select(ConvertToNumber)
            .Aggregate(0, (total, next) => next + total);

        return result.ToString();
    }

    internal static string GetIntegersFromString(string line) => string.Join("", line.ToCharArray().Where(Char.IsNumber));

    internal static string GetFirstAndLastNumeric(string numbersArray) => numbersArray.Length == 1 ? $"{numbersArray[0]}{numbersArray[0]}" : $"{numbersArray[0]}{numbersArray[numbersArray.Length - 1]}";

    internal static int ConvertToNumber(string number) => Convert.ToInt32(number);

    internal static string StringNumberToInteger(string line) => new Dictionary<string, string>() { { "one", "one1one" }, { "two", "two2two" }, { "three", "three3three" }, { "four", "four4four" }, { "five", "five5five" }, { "six", "six6six" }, { "seven", "seven7seven" }, { "eight", "eight8eight" }, { "nine", "nine9nine" }, }.Aggregate(line, (result, next) => result.Replace(next.Key, next.Value));
}