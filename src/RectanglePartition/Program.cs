using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    private int w;
    private int h;
    private int[] sliceX;
    private int[] sliceY;

    public Solution(int w, int h, int[] sliceX, int[] sliceY)
    {
        this.w = w;
        this.h = h;
        this.sliceX = sliceX;
        this.sliceY = sliceY;
    }

    public string Solve() {
        List<Rectangle> rects = new List<Rectangle>();
        Rectangle r = new Rectangle(0, 0, w, h);
        for (int i=0; i < sliceX.Length; i++) {
            for (int j=0; j < sliceY.Length; j++) {
                var rect = new Rectangle(0, 0, sliceX[i], sliceY[i]);
            }
        }
        return string.Empty;
    }

    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int w = int.Parse(inputs[0]);
        int h = int.Parse(inputs[1]);
        int countX = int.Parse(inputs[2]);
        int countY = int.Parse(inputs[3]);
        inputs = Console.ReadLine().Split(' ');
        int[] sliceX = new int[countX];
        int [] sliceY = new int[countY];
        for (int i = 0; i < countX; i++)
        {
            sliceX[i] = int.Parse(inputs[i]);
        }
        inputs = Console.ReadLine().Split(' ');
        for (int i = 0; i < countY; i++)
        {
            sliceY[i] = int.Parse(inputs[i]);
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");
        Solution solution = new Solution(w, h, sliceX, sliceY);

        Console.WriteLine(solution.Solve());
    }
}