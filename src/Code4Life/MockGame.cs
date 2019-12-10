using System.Collections.Generic;
using System.IO;

namespace Code4Life {
    public class MockGame : IGame {
        List<Sample> samples;
        private TextReader input;

        public MockGame() {
            samples = new List<Sample>();
        }
    }
}