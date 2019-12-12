using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace IsbnCheckDigit
{

    public class Solution
    {
        private string[] values;

        public Solution(string[] values)
        {
            this.values = values;
        }

        public string Solve()
        {
            return string.Empty;
        }

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            string[] values = new string[N];
            for (int i = 0; i < N; i++)
            {
                values[i] = Console.ReadLine();
            }

            var sln = new Solution(values);

            Console.WriteLine(sln.Solve());
        }
    }
}