using System;
using System.IO;
using System.Linq;

Console.WriteLine("Which day do you want to solve?");
var selectedDay = Console.ReadLine();

switch (selectedDay)
{
    case "1":
        One();
        break;
    case "2":
        Two();
        break;
    default:
        throw new ArgumentOutOfRangeException();
}

void One()
{
    var input = File.ReadAllLines("./one.txt").Select(x => Convert.ToInt32(x)).ToList();
    var count = 0;

    for (var i = 1; i < input.Count; i++)
    {
        if (input[i] > input[i - 1])
        {
            count++;
        }
    }
    Console.WriteLine($"Day1a: {count}");

    var windows = input.Select((i, index) =>
    {
        try
        {
            return i + input[index + 1] + input[index + 2];
        }
        catch (ArgumentOutOfRangeException)
        {
            return -1;
        }
    }).Where(i => i > 0).ToList();

    count = 0;
    for (var i = 1; i < windows.Count; i++)
    {
        if (windows[i] > windows[i - 1])
        {
            count++;
        }
    }

    Console.WriteLine($"Day1b: {count}");
}

void Two()
{
    var instructions = File.ReadAllLines("./two.txt")
        .Select(line => (command: line.Split(' ')[0], steps: Convert.ToInt32(line.Split(' ')[1])))
        .ToList();

    var (horizontal, vertical) = (0, 0);

    foreach (var (command, steps) in instructions)
    {
        switch (command)
        {
            case "forward":
                horizontal += steps;
                break;
            case "down":
                vertical += steps;
                break;
            case "up":
                vertical -= steps;
                break;
        }
    }

    var result = horizontal * vertical;
    Console.WriteLine($"Day2a: {result}");
    
    
    var (horizontalB, verticalB, aim) = (0, 0, 0);

    foreach (var (command, steps) in instructions)
    {
        switch (command)
        {
            case "forward":
                horizontalB += steps;
                verticalB += aim * steps;
                break;
            case "down":
                aim += steps;
                break;
            case "up":
                aim -= steps;
                break;
        }
    }
    
    var resultB = horizontalB * verticalB;
    Console.WriteLine($"Day2a: {resultB}");
}