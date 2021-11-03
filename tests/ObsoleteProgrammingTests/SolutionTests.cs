using NUnit.Framework;

using System;
using System.IO;
using System.Text;

namespace ObsoleteProgrammingTests
{
    public class SolutionTests
    {
        StringBuilder builder;
        TextReader _oldIn;
        TextWriter _oldOut;

        [SetUp]
        public void Setup()
        {
            builder = new StringBuilder();
            _oldIn = Console.In;
            _oldOut = Console.Out;
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetIn(_oldIn);
        }

        [Test]
        public void ArithmeticTest()
        {
            // Arrange
            var input = @"6
10 5 ADD OUT
10 5 SUB OUT
12 24 SUB OUT
30 10 MUL OUT
50 7 DIV OUT
50 7 MOD OUT";
            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter(builder));

            // Act
            Solution.Main(new string[0]);
            Console.SetOut(_oldOut);

            var answer = builder.ToString();
            Assert.AreEqual(@"15
5
-12
300
7
1
", answer);
        }

        [Test]
        public void StackManipulations()
        {
            // Arrange
            var input = @"4
101 200 854 POP SWP DUP OUT OUT OUT
9 76 100 ROT OUT OUT OUT
5 4 OVR OUT OUT OUT
0 1 4 ROT ROT DUP ROT ADD ROT OUT OUT OUT";
            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter(builder));

            // Act
            Solution.Main(new string[0]);
            Console.SetOut(_oldOut);

            var answer = builder.ToString();
            Assert.AreEqual(@"101
101
200
9
100
76
5
4
5
4
1
1
", answer);
        }

        [Test]
        public void Logic()
        {
            // Arrange
            var input = @"5
401 13 NOT NOT OUT OUT
402 178 NOT OUT OUT
403 89 POS OUT OUT
404 0 POS OUT OUT
405 -56 POS OUT OUT";
            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter(builder));

            // Act
            Solution.Main(new string[0]);
            Console.SetOut(_oldOut);

            var answer = builder.ToString();
            Assert.AreEqual(@"1
401
0
402
1
403
1
404
0
405
", answer);
        }

        [Test]
        public void SimpleFunctionSquare()
        {
            // Arrange
            var input = @"2
DEF SQ DUP MUL END
4 SQ OUT 10 SQ OUT 71 SQ OUT";
            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter(builder));

            // Act
            Solution.Main(new string[0]);
            Console.SetOut(_oldOut);

            var answer = builder.ToString();
            Assert.AreEqual(@"16
100
5041
", answer);
        }

        [Test]
        public void FunctionAndTest()
        {
            // Arrange
            var input = @"4
DEF MAX OVR OVR SUB POS NOT IF SWP FI POP END
5 3 MAX 3 7 MAX MAX -2 MAX 4 MAX OUT
-8 -3 -7 2 -4 MAX MAX MAX MAX OUT
0 1 MAX 13 MAX DUP OUT 20 MAX 7 MAX OUT";
            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter(builder));

            // Act
            Solution.Main(new string[0]);
            Console.SetOut(_oldOut);

            var answer = builder.ToString();
            Assert.AreEqual(@"7
2
13
20
", answer);
        }

        [Test]
        public void FunctionCallingFunctionAndNestedIf()
        {
            // Arrange
            var input = @"17
DEF ABS DUP POS NOT IF 0 SWP SUB FI END
51 ABS OUT -5 ABS OUT 0 ABS OUT
DEF NZ
  OVR ABS OVR ABS SUB
  DUP NOT
  IF POP DUP POS IF SWP FI
  ELS
    POS IF SWP FI
  FI
  POP
END
1 -2 NZ -8 NZ 4 NZ 5 NZ OUT
-12 -5 NZ -137 NZ OUT
42 -5 NZ 12 NZ 21 NZ 5 NZ 24 NZ OUT
42 5 NZ 12 NZ 21 NZ -5 NZ 24 NZ OUT
-5 -4 NZ -2 12 NZ NZ -40 4 NZ 2 18 NZ NZ NZ
11 5 NZ NZ OUT";
            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter(builder));

            // Act
            Solution.Main(new string[0]);
            Console.SetOut(_oldOut);

            var answer = builder.ToString();
            Assert.AreEqual(@"51
5
0
1
-5
5
5
2
", answer);
        }

        [Test]
        public void TheQueenOfFunctions()
        {
            // Arrange
            var input = @"5
DEF F1
  DUP 2 SUB POS 
  IF DUP 1 SUB F1 MUL ELS POP 1 FI 
END
7 F1 OUT 10 F1 OUT 0 F1 OUT 3 F1 OUT";
            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter(builder));

            // Act
            Solution.Main(new string[0]);
            Console.SetOut(_oldOut);

            var answer = builder.ToString();
            Assert.AreEqual(@"5040
3628800
1
6
", answer);
        }

        [Test]
        public void IHaveNoLoopAndIMustIterate()
        {
            // Arrange
            var input = @"8
DEF SQ DUP MUL END
DEF PL DUP OUT SQ OUT END
DEF PR OVR PL
  SWP 1 ADD OVR OVR SUB POS 
  IF SWP PR ELS POP POP FI
END
1 5 PR
30 33 PR";
            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter(builder));

            // Act
            Solution.Main(new string[0]);
            Console.SetOut(_oldOut);

            var answer = builder.ToString();
            Assert.AreEqual(@"1
1
2
4
3
9
4
16
5
25
30
900
31
961
32
1024
33
1089
", answer);
        }

        [Test]
        public void HelloFibonacci()
        {
            // Arrange
            var input = @"6
DEF RFIB DUP 
  IF 1 SUB ROT ROT DUP ROT ADD ROT RFIB 
  ELS POP POP FI 
END
DEF FIB 0 1 ROT RFIB END
5 FIB OUT 6 FIB OUT 2 FIB OUT 10 FIB OUT";
            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter(builder));

            // Act
            Solution.Main(new string[0]);
            Console.SetOut(_oldOut);

            var answer = builder.ToString();
            Assert.AreEqual(@"5
8
1
55
", answer);
        }

        [Test]
        public void IntegerSquareRoot()
        {
            // Arrange
            var input = @"20
DEF ESTIM DUP 4 SUB POS 
  IF 4 DIV SWP 2 MUL SWP ESTIM
  ELS POP
  FI
END
DEF NA SWP OVR DIV ADD 2 DIV END
DEF RS
  OVR OVR NA
  OVR OVR SUB DUP 1 SUB MUL
  IF
    SWP POP RS
  ELS
    ROT POP OVR OVR SUB POS IF SWP FI POP
  FI
END
DEF ASQRT
  DUP 1 SWP ESTIM RS
END
5040 ASQRT OUT 10 ASQRT OUT
1001 ASQRT OUT 65000 ASQRT OUT";
            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter(builder));

            // Act
            Solution.Main(new string[0]);
            Console.SetOut(_oldOut);

            var answer = builder.ToString();
            Assert.AreEqual(@"70
3
31
254
", answer);
        }
    }
}