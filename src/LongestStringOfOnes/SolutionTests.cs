using NUnit.Framework;

namespace LongestStringOfOnes
{
    public class SolutionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TwoBits()
        {
            var sln = new Solution("00");
            Assert.AreEqual(1, sln.Solve());
        }

        [Test]
        public void FiveBits()
        {
            var sln = new Solution("01010");
            Assert.AreEqual(3, sln.Solve());
        }

        [Test]
        public void ElevenBits()
        {
            var sln = new Solution("11011101111");
            Assert.AreEqual(8, sln.Solve());
        }

        [Test]
        public void FiftyBits()
        {
            var sln = new Solution("11100101100000111011101000000110100101111110111100");
            Assert.AreEqual(11, sln.Solve());
        }

        [Test]
        public void OneHundredBits()
        {
            var sln = new Solution("1011010111110110111001001110111011000001011010110011101101111101111111011100011001101011011110001010");
            Assert.AreEqual(13, sln.Solve());
        }

        [Test]
        public void NineNineNineBits()
        {
            var sln = new Solution("010001100101101100111010110010111000111010011101100000001110100100000111011100110001011100011011110011010111110101100000110001011010011011000000011000101001011101101110011100000110101100001001111011000000001000110011000001111110000010111001111000100010100001100011111001111011110101000010001101101001000000001010000101010101101011101000000010101110111010100001000010011100010110111111111010001000110010010110100110011101001110001000101001111111111111011111111101110110000001001110010010110100001101010010010101011010010001111001000010100110110100011000110100101111001011111100101110001010100100010010111100101110111011010100110011010001101011000000111110011010010100010010100111000010111000100110010100110111010101111001110111011011000100110100100101010110110001000100111110000111001110100110101100011010001101001101100010110100110110011101000000111001010101100011010111110100000100000011111110000111101001001110001000100101011011000000111100000111110010011000101001001100000110110111010111011110100");
            Assert.AreEqual(23, sln.Solve());
        }

        [Test]
        public void ThirtyTwoBits()
        {
            var sln = new Solution("10000000011100000000011111000001");
            Assert.AreEqual(6, sln.Solve());
        }

        [Test]
        public void TwentyBits()
        {
            var sln = new Solution("00101001100011001111");
            Assert.AreEqual(5, sln.Solve());
        }

        [Test]
        public void WholeString()
        {
            var sln = new Solution("111111101111111");
            Assert.AreEqual(15, sln.Solve());
        }

        [Test]
        public void AllZeroes()
        {
            var sln = new Solution("00000000000000000000");
            Assert.AreEqual(1, sln.Solve());
        }

        [Test]
        public void Gaps()
        {
            var sln = new Solution("1110011100111");
            Assert.AreEqual(4, sln.Solve());
        }
    }
}