using System;
using NUnit.Framework;

namespace RockPaperScissorsLizardSpock.Test
{
    public class SolutionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SameAsExample()
        {
            string[] players = new[] {"4 R", "1 P", "8 P", "3 R", "7 C", "5 S", "6 L", "2 L"};
            var sol = new Solution(players);
            Assert.AreEqual($"2{Environment.NewLine}6 5 1", sol.Solve());
        }

        [Test]
        public void With2Players()
        {
            string[] players = new[] {"1 S", "2 S"};
            var sol = new Solution(players);
            Assert.AreEqual($"1{Environment.NewLine}2", sol.Solve());
        }

        [Test]
        public void With32Players()
        {
            var players = new string[]
            {
                "28 R",
                "3 R",
                "13 L",
                "6 P",
                "32 C",
                "5 R",
                "11 S",
                "27 S",
                "22 L",
                "31 R",
                "30 R",
                "10 P",
                "18 R",
                "23 R",
                "8 R",
                "20 S",
                "7 P",
                "19 P",
                "26 P",
                "4 R",
                "16 C",
                "21 P",
                "1 C",
                "14 C",
                "29 R",
                "9 P",
                "25 C",
                "24 P",
                "15 R",
                "2 L", "12 L", "17 S"
            };
            var sol = new Solution(players);
            Assert.AreEqual($"10{Environment.NewLine}30 31 20 11 15", sol.Solve());
        }

        [Test]
        public void OnlyRockAndPaper()
        {
            string[] players =
            {
                "35 R",
                "66 R",
                "68 R",
                "81 R",
                "27 R",
                "88 R",
                "74 R",
                "125 R",
                "116 R",
                "9 R",
                "115 R",
                "4 R",
                "52 R",
                "111 R",
                "103 R",
                "77 R",
                "114 R",
                "71 R",
                "113 R",
                "100 R",
                "112 R",
                "3 R",
                "85 R",
                "57 R",
                "13 R",
                "60 R",
                "47 R",
                "31 R",
                "122 R",
                "50 R",
                "44 R",
                "106 R",
                "86 R",
                "65 R",
                "22 R",
                "37 R",
                "26 R",
                "43 R",
                "55 R",
                "42 R",
                "23 R",
                "45 R",
                "89 R",
                "91 R",
                "28 R",
                "63 R",
                "18 R",
                "67 R",
                "34 R",
                "127 R",
                "107 R",
                "41 R",
                "36 R",
                "61 R",
                "97 R",
                "87 R",
                "118 R",
                "110 R",
                "96 R",
                "40 R",
                "14 R",
                "102 R",
                "84 R",
                "126 R",
                "117 R",
                "83 R",
                "101 R",
                "80 R",
                "58 R",
                "82 R",
                "119 R",
                "72 R",
                "51 R",
                "21 R",
                "33 R",
                "8 R",
                "1 R",
                "7 R",
                "92 R",
                "25 R",
                "16 R",
                "30 R",
                "79 R",
                "46 R",
                "94 R",
                "120 R",
                "59 R",
                "121 R",
                "108 R",
                "69 R",
                "73 R",
                "124 R",
                "12 R",
                "93 R",
                "78 R",
                "5 R",
                "29 R",
                "70 R",
                "109 R",
                "48 R",
                "64 R",
                "76 R",
                "38 R",
                "104 R",
                "75 R",
                "128 P",
                "20 R",
                "2 R",
                "95 R",
                "62 R",
                "10 R",
                "56 R",
                "99 R",
                "39 R",
                "105 R",
                "19 R",
                "15 R",
                "17 R",
                "54 R",
                "90 R",
                "6 R",
                "98 R",
                "123 R",
                "49 R",
                "32 R",
                "11 R",
                "53 R",
                "24 R"
            };
            var sol = new Solution(players);
            Assert.AreEqual($"128{Environment.NewLine}75 2 10 29 6 1 3", sol.Solve());
        }
    }
}