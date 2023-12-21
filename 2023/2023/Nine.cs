using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("2023.Tests")]

public class Nine : BaseLevel, ILevel
{
    internal static int[] CalculateDifferences(int[] input) =>
        input.Select((v, i) => i < input.Length - 1 ? input[i + 1] - v : -1337).Where(v => v != -1337).ToArray();

    internal static int[][] LoopUntilAllZero(int[] ints)
    {
        var result = Array.Empty<int[]>();
        var current = ints;

        while(!current.All(i => i == 0)) {
            result = result.Append(current).ToArray();
            current = CalculateDifferences(current);
        }

        return result;
    }

    internal static int[][] Parse(string[] input) =>
        input.Select(i => i.Split(" ").Select(s => Convert.ToInt32(s)).ToArray()).ToArray();

    public string SolveA()
    {
        return string.Empty;
    }


    public string SolveB()
    {
        return string.Empty;
    }


  
}



