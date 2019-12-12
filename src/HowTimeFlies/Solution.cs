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
        public Solution(string begin, string end)
        {
            var parts = begin.Split(".".ToCharArray());
            _start = new DateTime(int.Parse(parts[2]), int.Parse(parts[1]), int.Parse(parts[0]));
            parts = end.Split(".".ToCharArray());
            _end = new DateTime(int.Parse(parts[2]), int.Parse(parts[1]), int.Parse(parts[0]));
        }
        public string Solve()
        {
            Console.Error.WriteLine($"Start: {_start}, End: {_end}");
            var diff = _end - _start;
            Console.Error.WriteLine($"Diff: {diff}");
            DateTime next;
            var years = CountYears(out next);
            var months = CountMonths(next);
            int days = (int)diff.TotalDays;
            Console.Error.WriteLine($"Years: {years}, Months: {months}, Days: {days}");
            string result = "";
            if (years > 0)
            {
                result += $"{years} year{((years > 1)?"s":"")}, ";
            }
            if (months > 0)
            {
                result += $"{months} month{((months > 1)?"s":"")}, ";
            }
            return result + $"total {days} days";
        }

        private int CountYears(out DateTime next)
        {
            int result = 0;
            while (_start.AddYears(result) <= _end)
            {
                result++;
            }
            next = _start.AddYears(result - 1);
            return result-1;
        }
        private int CountMonths(DateTime start)
        {
            int result = 0;
            while (start.AddMonths(result) <= _end)
            {
                result++;
            }
            return result - 1;
        }
        static void Main(string[] args)
        {
            string BEGIN = Console.ReadLine();
            string END = Console.ReadLine();

            var sln = new Solution(BEGIN, END);

            Console.WriteLine(sln.Solve());
        }
    }
}
