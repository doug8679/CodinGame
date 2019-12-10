using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    /*static void Main(string[] args)
    {
        int R = int.Parse(Console.ReadLine());
        int V = int.Parse(Console.ReadLine());
        for (int i = 0; i < V; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int C = int.Parse(inputs[0]);
            int N = int.Parse(inputs[1]);
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine("1");
    }*/

    private int nRobbers;
    private int nVaults;
    private List<int[]> _combos;

    public Solution(int R, int V, List<int[]> combos) {
        nRobbers = R;
        nVaults = V;
        _combos = combos;
    }

    public string Solve() {
        double solution = 0;
        var VperR = nVaults / nRobbers;
        foreach(var combo in _combos) {
            int chars = combo[0] - combo[1];
            solution += System.Math.Pow(10, combo[1]) * System.Math.Pow(5, chars);
        }
        return $"{solution}";
    }
}