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
    }
}