using System;
using System.Collections.Generic;
using System.Text;

namespace  SnowflakeId.Core
{
    public static class Base36Converter {
        private static string _charList = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// The character set for encoding. Defaults to upper-case alphanumerics 0-9, A-Z.
        /// </summary>
        public static string CharList
        {
            get {
                return _charList;
            } set {
                _charList = value;
            }
        }

        public static string Convert(string number, int fromBase, int toBase)
        {
            // var digits = "0123456789abcdefghijklmnopqrstuvwxyz";
            int length = number.Length;
            string result = string.Empty;


            char[] newCharArray = number.ToCharArray();
            int newlen;

            List<int> nibbles = new List<int>();

            foreach (char c in newCharArray) {
                nibbles.Add((int) c);
            }

            do {
                int value = 0;
                newlen = 0;

                for (int i = 0; i < length; ++i) {
                    value = value * fromBase + nibbles[i];

                    if (value >= toBase) {
                        if (newlen == nibbles.Count) {
                            nibbles.Add(0);
                        }

                        nibbles[newlen++] = value / toBase;
                        value %= toBase;
                    } else if (newlen > 0) {
                        if (newlen == nibbles.Count) {
                            nibbles.Add(0);
                        }

                        nibbles[newlen++] = 0;
                    }
                }

                length = newlen;
                result = CharList[value] + result;
            } while (newlen != 0);

            return result;
        }

        public static string FromHex(string hex)
        {
            return Base36Converter.Convert(hex, 16, 36);
        }
        public static string FromGuid(Guid guid)
        {
            return Base36Converter.Convert(guid.ToString("N"), 16, 36);
        }
        public static string FromInt32(long int32)
        {
            return Base36Converter.Convert(int32.ToString(), 10, 36);
        }
        public static string FromInt64(long int64)
        {
            return Base36Converter.Convert(int64.ToString(), 10, 36);
        }
        /// <summary>
        /// Encode the given number into a Base36 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Encode(long input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");

            char[] clistarr = CharList.ToCharArray();
            Stack<char> result = new Stack<char>();

            while (input != 0) {
                result.Push(clistarr[input % 36]);
                input /= 36;
            }

            return new string(result.ToArray());
        }

        /// <summary>
        /// Decode the Base36 Encoded string into a number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long Decode(string input)
        {
            char[] newCharArray = input.ToCharArray();
            Array.Reverse(newCharArray);
            long result = 0;
            int pos = 0;

            foreach (char c in newCharArray) {
                result += CharList.IndexOf(c) * (long) Math.Pow(36, pos);
                pos++;
            }

            return result;
        }
    }
}
