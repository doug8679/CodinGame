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
            StringBuilder result = new StringBuilder();
            List<string> invalid = new List<string>();
            foreach (string value in values)
            {
                if (!Valid(value))
                {
                    invalid.Add(value);
                }
            }
            result.AppendLine($"{invalid.Count} invalid:");
            for (int i=0; i<invalid.Count-1; i++)
            {
                result.AppendLine(invalid[i]);
            };
            result.Append(invalid[invalid.Count-1]);
            return result.ToString();
        }

        private bool Valid(string value)
        {
            if (value.Contains("X") && value.IndexOf("X") != value.Length-1)
                return false;
            if (value.Length == 10 || value.Length == 13)
            {
                int modulo = (value.Length == 10) ? 11 : 10;
                int sum = 0;
                switch (modulo)
                {
                    case 11:
                        for (int i = 0; i < 9; i++)
                        {
                            sum += int.Parse(value.Substring(i, 1)) * (10 - i);
                        }
                        break;
                    case 10:
                        for (int i = 0; i < 12; i++)
                        {
                            sum += int.Parse(value.Substring(i, 1)) * (i % 2 == 0 ? 1 : 3);
                        }
                        break;
                }
                int rem = sum % modulo;
                if (rem > 0)
                {
                    rem = modulo - rem;
                }
                string check = $"{rem}";
                check = (check.Equals("10") && modulo == 11) ? "X" : check;
                string digit = value.Substring(value.Length - 1, 1);
                return check.Equals(digit);
            }
            return false;
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