using System;
using System.Linq;

namespace ANEOSponsoredPuzzle_001
{
    public class Solution
    {
        private int max_speed;
        private float[][] lights;

        public Solution(int speed, float[][] lights)
        {
            max_speed = speed;
            this.lights = lights;
        }

        public static void Main(string[] args)
        {
            int speed = int.Parse(Console.ReadLine());
            int lightCount = int.Parse(Console.ReadLine());
            float[][] lights = new float[lightCount][];
            for (int i = 0; i < lightCount; i++)
            {
                lights[i] = Console.ReadLine().Split(' ').Select(s => float.Parse(s)).ToArray();
            }

            var sol = new Solution(speed, lights);

            // Write an answer using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine(sol.Solve());
        }

        private int Solve()
        {
            int result = 1;
            
            for (int i=max_speed; i>0; i--)
            {
                var allGreen = lights.Select(l => ((int)(Math.Round(l[0] / (i / 3.6), 0) / l[1]) % 2 == 0)).ToArray();
                if (allGreen.All(g => g)) 
                { 
                    return i;
                }
            }
            
            return result;
        }
    }
}
