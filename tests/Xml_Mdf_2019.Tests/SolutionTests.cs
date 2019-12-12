using NUnit.Framework;

namespace Xml_Mdf_2019.Tests
{
    public class SolutionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OnlyOne()
        {
            var sln = new Solution("aa-aa-a-a");
            var answer = sln.Solve();
            Assert.AreEqual("a", answer);
        }

        [Test]
        public void NestedMightLose()
        {
            var sln = new Solution("ab-b-a");
            var answer = sln.Solve();
            Assert.AreEqual("a", answer);
        }

        [Test]
        public void SeveralNestedMightWin()
        {
            var sln = new Solution("ab-bb-bb-b-a");
            var answer = sln.Solve();
            Assert.AreEqual("b", answer);
        }

        [Test]
        public void SeveraslDeeperNestedMightWinToo()
        {
            var sln = new Solution("abc-cc-c-bc-cc-c-a");
            var answer = sln.Solve();
            Assert.AreEqual("c", answer);
        }

        [Test]
        public void OnlyOneShalWin()
        {
            var sln = new Solution("abc-cc-cc-cc-c-bd-dd-d-a");
            var answer = sln.Solve();
            Assert.AreEqual("c", answer);
        }

        [Test]
        public void SizeShouldNotMatter()
        {
            var sln = new Solution("nu-u-nim-mo-o-irjlncx-xzd-d-z-cg-gma-a-m-n-l-j-rff-f-fo-onkbwn-nf-f-wra-a-rlbaf-ftesov-v-o-sld-d-l-e-t-a-b-lch-ha-aw-w-c-bxt-t-x-k-net-t-eoif-f-i-ofe-eka-a-kc-c-fyvv-v-v-yx-xsjuuaf-fd-d-a-u-u-j-suewunq-q-n-u-w-er-rmd-dum-mhq-quo-o-utip-p-ivgcm-m-cg-g-gt-t-v-t-h-u-m-u");
            var answer = sln.Solve();
            Assert.AreEqual("f", answer);
        }
    }
}