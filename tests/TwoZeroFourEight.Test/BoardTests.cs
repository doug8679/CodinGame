using System;
using NUnit.Framework;

namespace TwoZeroFourEight.Test
{
    public class BoardTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestSeed290797()
        {
            Board b = new Board(290797);
            var actual = b.ToString();
            var expected = $"0 0 0 0{Environment.NewLine}0 0 0 2{Environment.NewLine}0 0 0 0{Environment.NewLine}0 4 0 0{Environment.NewLine}";
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(13339144, b.Seed);
        }

        [Test]
        public void TestCloneWithMove()
        {
            Board b = new Board(290797);
            
            Console.WriteLine($"{b}");
            Assert.AreEqual(13339144, b.Seed);

            var nextSet = new[] {b.CloneWithMove(0), b.CloneWithMove(1), b.CloneWithMove(2), b.CloneWithMove(3)};
            for (var i = 0; i < nextSet.Length; i++)
            {
                Console.WriteLine($"Move Dir: {Player.DIRECTIONS[i]}{Environment.NewLine}{nextSet[i]}");
            }
        }
    }
}
