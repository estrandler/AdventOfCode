using System;
using System.IO;
using System.Linq;


One();

void One()
{
    var input = File.ReadAllLines("./one.txt").Select(x => Convert.ToInt32(x)).ToList();
    var count = 0;
    
    for(var i = 1; i < input.Count; i++)
    {
        if (input[i] > input[i - 1])
        {
            count++;
        }
    }

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
    for(var i = 1; i < windows.Count; i++)
    {
        if (windows[i] > windows[i - 1])
        {
            count++;
        }
    }
    Console.WriteLine($"Day1b: {count}");
}