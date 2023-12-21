using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("2023.Tests")]

public class Eight : BaseLevel, ILevel
{

    public string SolveA()
    {
        var (instructions, nodes) = Parse(GetInput());
        var nodesVisited = Eight.IterateNodesUntilDone(instructions, nodes, "AAA", "ZZZ");

        return (nodesVisited.Count - 1).ToString();
    }


    public string SolveB()
    {
        var (instructions, nodes) = Parse(GetInput());
        var startingNodes = FindStartingNodes(nodes);
        var endingNotes = FindEndingNodes(nodes);

        var combinations = startingNodes.SelectMany(g => endingNotes.Select(c => new { Start = g, End = c }));

        var dictionary = new Dictionary<string, List<int>>();

        foreach (var combination in combinations)
        {
            Console.WriteLine($"Combination start {combination.Start}, target {combination.End}");
            var nodesVisited = Eight.IterateNodesUntilDone(instructions, nodes, combination.Start, combination.End);

            if (dictionary.ContainsKey(combination.Start.ToString()))
            {
                dictionary[combination.Start.ToString()].Add(nodesVisited.Count - 1);
            } else
            {
                dictionary[combination.Start.ToString()] = new List<int>() { nodesVisited.Count - 1 };
            }
        }

        return dictionary.ToString();
    }


    internal static string GetNextPosition(string instruction, string left, string right)
        => instruction == "L" ? left : right;


    internal static (string left, string right) GetNodeFromKey(string key, Dictionary<string, (string left, string right)> positions)
        => positions[key];

    internal static (string[] instructions, Dictionary<string, (string left, string right)> nodes) Parse(string[] lines)
    {
        var instructionsResult = Array.Empty<string>();
        var nodesResult = new Dictionary<string, (string left, string right)>();

        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                instructionsResult = lines[i].ToCharArray().Select(x => x.ToString()).ToArray();
                continue;
            }
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }

            var regex = new Regex("[A-Z]{3}");
            var matches = regex.Matches(lines[i]);

            nodesResult.Add(matches[0].Value, (matches[1].Value, matches[2].Value));
        }

        return (instructionsResult, nodesResult);
    }

    internal static string GetNextInstruction(int i, string[] instructions)
    {
        if (i < instructions.Length)
        {
            return instructions[i];
        }

        return GetNextInstruction(i, instructions.Concat(instructions).ToArray());
    }

    internal static List<string> IterateNodesUntilDone(string[] instructions, Dictionary<string, (string left, string right)> nodes, string startKey, string endKey)
    {
        var currentKey = startKey;
        var i = 0;
        var visited = new List<string>()
        {
            currentKey
        };

        while (!currentKey.EndsWith(endKey))
        {
            var nextInstruction = GetNextInstruction(i, instructions);
            var node = GetNodeFromKey(currentKey, nodes);
            currentKey = GetNextPosition(nextInstruction, node.left, node.right);

            visited.Add(currentKey);
            i++;
        }

        return visited;
    }

    internal static string[] FindStartingNodes(Dictionary<string, (string left, string right)> nodes)
        => nodes.Where(x => x.Key.EndsWith("A")).Select(x => x.Key).ToArray();
    internal static string[] FindEndingNodes(Dictionary<string, (string left, string right)> nodes) =>
        nodes.Where(s => s.Key.EndsWith("Z")).Select(s => s.Key).ToArray();

    internal static bool AllEndOnZ(string[] nodes)
        => nodes.All(x => x.EndsWith("Z"));

    internal static int IterateNodesUntilDone(string[] instructions, string[] startingNodes, string endingNotes, Dictionary<string, (string left, string right)> nodes)
    {
        var currentNodes = startingNodes;
        var i = 0;

        while (!AllEndOnZ(currentNodes))
        {
            var nextInstruction = GetNextInstruction(i, instructions);
            var next = currentNodes.Select(currentNode =>
            {
                var node = GetNodeFromKey(currentNode, nodes);
                var nextPosition = GetNextPosition(nextInstruction, node.left, node.right);

                return nextPosition;
            }).ToArray();

            currentNodes = next;

            i++;
        }

        return i;
    }
}



