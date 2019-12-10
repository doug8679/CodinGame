using System;
using System.Linq;

namespace EnigmaMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            string operation = Console.ReadLine();
            int pseudoRandomNumber = int.Parse(Console.ReadLine());
            string[] rotor = new string[3];
            for (int i = 0; i < 3; i++) {
                rotor[i] = Console.ReadLine();
            }
            string message = Console.ReadLine();
            Console.Error.WriteLine($"Original Message: {message}");

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            string result = string.Empty;
            if (operation.Equals("ENCODE")) {
                result = Encode(pseudoRandomNumber, rotor, message);
                Console.Error.WriteLine($"Encoded Message: {result}");
            } else {
                result = Decode(pseudoRandomNumber, rotor, message);
                Console.Error.WriteLine($"Decoded message: {result}");
            }
            
            Console.WriteLine(result);
        }

        private static string Encode(int shift, string[] rotor, string message) {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] shiftedMessage = new char[message.Length];
            char[] result = new char[message.Length];

            for (int i=0; i<message.Length; i++) {
                int idx = alphabet.IndexOf(message[i]) + shift + i;
                while (idx >= alphabet.Length) {
                    idx -= alphabet.Length;
                }
                shiftedMessage[i] = alphabet[idx];
            }
            Console.Error.WriteLine($"Shifted Message: {string.Join("", shiftedMessage)}");

            foreach (var cipher in rotor)
            {
                for (int i=0; i<shiftedMessage.Length; i++) {
                    int idx = alphabet.IndexOf(shiftedMessage[i]);
                    shiftedMessage[i] = cipher[idx];
                }
                Console.Error.WriteLine($"After cipher '{cipher}', shifted message is: {string.Join("", shiftedMessage)}");
            }
            return string.Join("", shiftedMessage);
        }

        private static string Decode(int shift, string[] rotor, string message) {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] result = message.ToCharArray();

            for (int i=rotor.Length-1; i>=0; i--) {
                for (int j=0; j<result.Length; j++) {
                    result[j] = alphabet[rotor[i].IndexOf(result[j])];
                }
                Console.Error.WriteLine($"After cipher '{rotor[i]}', deciphered message is: {string.Join("", result)}");
            }

            for (int i=0; i<result.Length; i++) {
                int idx = alphabet.IndexOf(result[i]) - shift - i;
                while (idx < 0) {
                    idx += alphabet.Length;
                }
                result[i] = alphabet[idx];
            }
            
            return string.Join("", result);
        }
    }
}
