
var levels = new Dictionary<string, Func<string>>() {
    {"1a", () => new One().SolveA() },
    {"1b", () => new One().SolveB() },
    {"2a", () => new Two().SolveA() },
    {"2b", () => new Two().SolveB() },
    {"3a", () => new Three().SolveA() },
    {"3b", () => new Three().SolveB() },
    {"4a", () => new Four().SolveA() },
    {"4b", () => new Four().SolveB() },
};

while (true)
{
    Console.WriteLine($"Input which level to solve (or exit):");

    var input = Console.ReadLine();

    if (input == "exit")
        Environment.Exit(0);

    Console.WriteLine(levels[input]());
}


