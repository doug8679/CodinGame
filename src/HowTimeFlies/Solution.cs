using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace HowTimeFlies
{

public class Solution
{
    DateTime _start, _end;
    public Solution(string begin, string end) {
        _start = DateTime.Parse(begin);
        _end = DateTime.Parse(end);
    }
    public string Solve() {
        var diff = _end - _start;
        return $"{(int)(diff.TotalDays / 365)} years, total {diff.TotalDays} days";
    }
    static void Main(string[] args)
    {
        string BEGIN = Console.ReadLine();
        string END = Console.ReadLine();

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine("YY year[s], MM month[s], total NN days");
    }
}
}
