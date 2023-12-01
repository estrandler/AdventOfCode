
var levels = new Dictionary<string, Func<string>>() {
    {"1a", () => new One().SolveA() },
    {"1b", () => new One().SolveB() },
};

while (true)
{
    Console.WriteLine($"Input which level to solve (or exit):\r\n{string.Join("\r\n", levels.Keys.Select(k => k))}");

    var input = Console.ReadLine();

    if (input == "exit")
        Environment.Exit(0);

    Console.WriteLine(levels[input]());
}


