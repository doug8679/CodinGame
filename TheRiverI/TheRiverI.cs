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
            var meeting = River.MeetingPoint(new River(32), new River(47));
            Assert.AreEqual(47, meeting);
        }

        [Test]
        public void Test2()
        {
            var meeting = River.MeetingPoint(new River(57), new River(78));
            Assert.AreEqual(111, meeting);
        }

        [Test]
        public void Test3()
        {
            var meeting = River.MeetingPoint(new River(7), new River(5));
            Assert.AreEqual(620, meeting);
        }

        [Test]
        public void Test4()
        {
            var meeting = River.MeetingPoint(new River(483), new River(456));
            Assert.AreEqual(483, meeting);
        }
        [Test]
        public void Test5()
        {
            var meeting = River.MeetingPoint(new River(1158), new River(2058));
            Assert.AreEqual(2103, meeting);
        }

        [Test]
        public void Test6()
        {
            var meeting = River.MeetingPoint(new River(5026), new River(2489));
            Assert.AreEqual(5215, meeting);
        }

        [Test]
        public void Test7()
        {
            var meeting = River.MeetingPoint(new River(13), new River(14689));
            Assert.AreEqual(20014, meeting);
        }

        [Test]
        public void Test8()
        {
            var meeting = River.MeetingPoint(new River(991), new River(997));
            Assert.AreEqual(1118, meeting);
        }

        [Test]
        public void Test9()
        {
            var meeting = River.MeetingPoint(new River(15485863), new River(15215260));
            Assert.AreEqual(15490633, meeting);
        }
    }

    public class River
    {
        long start;
        long current;

        public River(long start) {
            this.start = start;
        }

        public long Next() {
            if (current < start) {
                current = start;
                return current;
            }
            long sum = current;
            foreach (var c in current.ToString()) {
                sum += long.Parse(c.ToString());
            }
            current = sum;
            return current;
        }

        public static long MeetingPoint(River r1, River r2) {
            long n1 = r1.Next();
            long n2 = r2.Next();
            while (n1 != n2) {
                if (n1 < n2) {
                    n1 = r1.Next();
                } else if (n2 < n1) {
                    n2 = r2.Next();
                }
            }
            return n1;
        }
    }
}