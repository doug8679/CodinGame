using System;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string test = "{([]){}()}";
            var result = Parser.TestExpression(test);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Test2()
        {
            string test = "{([{S}]]6K[()]}";
            var result = Parser.TestExpression(test);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Test3()
        {
            string test = "{C{}[{[a]}RqhL]{y2}}";
            var result = Parser.TestExpression(test);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Test4()
        {
            string test = "W12{}{}L{}";
            var result = Parser.TestExpression(test);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Test5()
        {
            string test = "h{Pn{GT{h}(c))}";
            var result = Parser.TestExpression(test);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Test6()
        {
            string test = "{[{iHTSc}]}p(R)m(){q({})";
            var result = Parser.TestExpression(test);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Test7()
        {
            string test = "][";
            var result = Parser.TestExpression(test);
            Assert.AreEqual(false, result);
        }
    }

    public class Parser
    {
        public static bool TestExpression(string test)
        {
            int openP=0,openC=0,openS=0;
            bool pOpen=false,cOpen=false,sOpen=false;
            bool startClosed=false;
            foreach (var item in test)
            {
                Console.Write($"{item}");
                switch (item) {
                    case '(':
                        openP++;
                        pOpen=true;
                        break;
                    case ')':
                        if (!pOpen && openP<=0) startClosed = true;
                        openP--;
                        pOpen=false;
                        break;
                    case '[':
                        openS++;
                        sOpen=true;
                        break;
                    case ']':
                        if (!sOpen && openS<=0) startClosed=true;
                        openS--;
                        sOpen=false;
                        break;
                    case '{':
                        openC++;
                        cOpen=true;
                        break;
                    case '}':
                        if(!cOpen  && openC<=0) startClosed = true;
                        openC--;
                        cOpen=false;
                        break;
                }
            }
            Console.WriteLine();

            Console.WriteLine($"{openP}, {openC}, {openS}, {startClosed}");
            return openP==0 && openC==0 && openS==0 && !startClosed;
        }
    }
}