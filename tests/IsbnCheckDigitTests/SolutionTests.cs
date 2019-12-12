using IsbnCheckDigit;
using NUnit.Framework;

namespace IsbnCheckDigitTests
{
    public class SolutionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Example()
        {
            var sln = new Solution(new string[] {"0306406152", "1145185215X", "9780306406157", "9780306406154", "978043551907X" });
            var answer = sln.Solve();
            Assert.AreEqual("3 invalid:" + System.Environment.NewLine +
                            "1145185215X" + System.Environment.NewLine +
                            "9780306406154" + System.Environment.NewLine +
                            "978043551907X", answer);
        }

        [Test]
        public void Short()
        {
            var sln = new Solution(new string[] { "0470371722", "9781119247792", "9780470124512", "11190495550", "1118105354", "9781118737637", "0387372350", "154875155X", "9781548751555", "978193981677" });
            var answer = sln.Solve();
            Assert.AreEqual("4 invalid:" + System.Environment.NewLine +
                            "9781119247792" + System.Environment.NewLine +
                            "11190495550" + System.Environment.NewLine +
                            "154875155X" + System.Environment.NewLine + 
                            "978193981677", answer);
        }

        [Test]
        public void Longer()
        {
            var sln = new Solution(new string[] { "0471697037", "1171711743", "1146386265", "9781531807382", "0671039414", "0553282964", "0743412524", "0553270502", "9781784399092", "0553493825", "9781511343572", "0553156284", "0553570099", "9780606393188", "97804X8458533", "9780745669779", "0976315262", "1498773192", "9780408478999", "9780691154305" });
            var answer = sln.Solve();
            Assert.AreEqual("5 invalid:" + System.Environment.NewLine +
                            "0471697037" + System.Environment.NewLine +
                            "0671039414" + System.Environment.NewLine +
                            "0553270502" + System.Environment.NewLine +
                            "97804X8458533" + System.Environment.NewLine +
                            "9780408478999", answer);
        }

        [Test]
        public void MuchLonger()
        {
            var sln = new Solution(new string[] { "0880221576", "1145185215", "9781843547587", "9781848873414", "9780470105542", "0471697036", "111842297X", "978111843111X", "0671039423", "0553493515", "0448484277", "9780134177304", "9781119056454", "0471379807", "047124371X", "9780134177296", "013708160X", "0471697044", "111843112X", "9783540684695", "039309202X", "9781592291236", "3794515471", "0980190207", "9781620322321", "0722532121", "9780198790563", "9783848419616", "0077660811", "0736911537", "1592290256", "9780538797825", "1780781004", "3764374829", "0139182276", "0195132912", "0314262687", "9783330328358", "9780723455585", "9780199658572", "9780133661750", "3510490738", "9780702068591", "0918860172", "1938168232", "0842309969", "0801401345", "3211249648", "143022889X", "0953169650" });
            var answer = sln.Solve();
            Assert.AreEqual("6 invalid:" + System.Environment.NewLine +
                            "0880221576" + System.Environment.NewLine +
                            "978111843111X" + System.Environment.NewLine +
                            "0471379807" + System.Environment.NewLine +
                            "9780134177296" + System.Environment.NewLine +
                            "1780781004" + System.Environment.NewLine +
                            "0801401345", answer);
        }
    }
}