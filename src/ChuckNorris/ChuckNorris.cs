using System;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string result = Processor.Process("C");
            Assert.AreEqual("0 0 00 0000 0 00", result);
        }

        [Test]
        public void Test2()
        {
            string result = Processor.Process("CC");
            Assert.AreEqual("0 0 00 0000 0 000 00 0000 0 00", result);
        }

        [Test]
        public void Test3()
        {
            string result = Processor.Process("%");
            Assert.AreEqual("00 0 0 0 00 00 0 0 00 0 0 0", result);
        }

        [Test]
        public void Test4()
        {
            string result = Processor.Process("Chuck Norris' keyboard has 2 keys: 0 and white space.");
            Assert.AreEqual("0 0 00 0000 0 0000 00 0 0 0 00 ...0 000 00 000 0 0000 00 00 0 0 00 0 0 0 00 0 0 0 00 0 0 000 00 0", result);
        }
    }

    public class Processor {
        public static string Process(string input) {
            string test = string.Empty;
            string pattern = "(?'ones'[1]+)|(?'zeroes'[0]+)";
            foreach (var c in input) {
                var val = (int)c;
                var add = Convert.ToString(val, 2);
                if (add.Length < 7) {
                    add = string.Join("", Enumerable.Repeat("0", 7-add.Length)) + add;
                }
                test += add;
            }
            Console.Error.WriteLine(test);
            MatchCollection matches = Regex.Matches(test, pattern);
            string result = string.Empty;
            foreach(Match match in matches) {
                if (match.Groups["ones"].Success)
                    result += "0 " + string.Join("", Enumerable.Repeat("0", match.Groups["ones"].Length)) + " ";
                if (match.Groups["zeroes"].Success)
                    result += "00 " + string.Join("", Enumerable.Repeat("0", match.Groups["zeroes"].Length)) + " ";
            }
            return result.Trim();
        }
    }
}