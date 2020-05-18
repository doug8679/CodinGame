using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using Microsoft.Extensions.FileProviders;
using NUnit.Framework;

namespace PlagueJunior.Test
{
    public class SolutionTests
    {
        private EmbeddedFileProvider _provider;
        private TextReader _oldIn;

        [SetUp]
        public void Setup()
        {
            _oldIn = Console.In;
            _provider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetIn(_oldIn);
        }

        [Test]
        public void TheExample()
        {
            StringBuilder b = new StringBuilder();
            using (var reader = new StreamReader(_provider.GetFileInfo("TheExample.txt").CreateReadStream()))
            using (var writer = new StringWriter(b))
            {
                Console.SetIn(reader);
                Console.SetOut(writer);
                Solution.Main(new string[0]);
                Assert.AreEqual("1", b.ToString());
            }
            Assert.Pass();
        }
    }
}