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
public class Solution
{
    private string content;
    public Solution(string input)
    {
        content = input;
    }

    public int Solve()
    {
        string[] pieces = content.Split("0".ToCharArray());
        int maxLen = 0;
        for (int i=1; i<pieces.Length; i++)
        {
            int len = pieces[i-1].Length + pieces[i].Length + 1;
            maxLen = Math.Max(maxLen, len);
        }

        return maxLen;
    }

    static void Main(string[] args)
    {
        var sln = new Solution(Console.ReadLine());
        Console.WriteLine(sln.Solve());
    }
}