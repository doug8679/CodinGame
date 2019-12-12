using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Xml_Mdf_2019
{

    public class Solution
    {
        Dictionary<char, Dictionary<int, int>> depths;
        private string v;

        public Solution(string v)
        {
            this.v = v;
            depths = new Dictionary<char, Dictionary<int, int>>();
        }

        public string Solve() {
            int depth = 1;
            for (int i=0; i<v.Length; i++)
            {
                if (v[i] == '-') {
                    depth--;
                    i++;
                } else
                {
                    if (!depths.TryGetValue(v[i], out var counts))
                    {
                        counts = new Dictionary<int, int>();
                        depths.Add(v[i], counts);
                    }
                    if (counts.ContainsKey(depth))
                    {
                        counts[depth]++;
                    } else
                    {
                        counts.Add(depth, 1);
                    }
                    depth++;
                }
            }
            double maxW=0;
            char owner = ' ';
            Dictionary<char, int> weigths = new Dictionary<char, int>();
            foreach(var pair in depths)
            {
                double weight = 0;
                foreach (var counts in pair.Value)
                {
                    Console.Error.WriteLine($"\tTag '{pair.Key}', Depth = {counts.Key}, Quantity = {counts.Value}");
                    weight += (1.0/counts.Key) * counts.Value;
                }
                Console.Error.WriteLine($"Tag '{pair.Key}' has weight = {weight}");
                if (weight > maxW)
                {
                    maxW = weight;
                    owner = pair.Key;
                }
            }

            return $"{owner}";
        }

        static void Main(string[] args)
        {
            var sln = new Solution(Console.ReadLine());
            Console.WriteLine(sln.Solve());
        }
    }
}