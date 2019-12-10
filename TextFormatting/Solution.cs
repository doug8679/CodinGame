using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace TextFormatting {

    public class Solution
    {
        private string original;
        public Solution(string input) {
            original = input.Trim();
        }

        public string Solve() {
            string result = "";
            var match = Regex.Match(original, "[^A-Za-z0-9]+");
            bool newSentence = true;
            int index = 0;
            while (match.Success)
            {
                string val = original.Substring(index, match.Index - index).ToLower();
                if (newSentence)
                    val = $"{char.ToUpper(val[0])}{val.Substring(1)}";

                result += val + EvaluateAndAdjust(match.Value, out newSentence);
                index = match.Index + match.Length;
                match = match.NextMatch();
            }

            if (index < original.Length)
            {
                string val = original.Substring(index).ToLower();
                if (newSentence)
                    val = $"{char.ToUpper(val[0])}{val.Substring(1)}";
                result += val;
            }
            Console.WriteLine($"'{result}'");
            return result.Trim();
        }

        private string EvaluateAndAdjust(string value, out bool newSentence)
        {
            newSentence = false;
            string result = " ";
            if (value.Contains(","))
            {
                result = ", ";
            } else if (value.Contains("."))
            {
                result = ". ";
                newSentence = true;
            }
            else if (value.Contains(";"))
            {
                result = "; ";
            }
            else if (value.Contains("!"))
            {
                result = "! ";
            }
            else if (value.Contains("?"))
            {
                result = "? ";
            }
            return result;
        }

        static void Main(string[] args)
        {
            string intext = Console.ReadLine();

            var sln = new Solution(intext);
            
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            
            Console.WriteLine(sln.Solve());
            Console.ReadLine();
        }
    }
}