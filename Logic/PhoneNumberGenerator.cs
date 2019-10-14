using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class PhoneNumberGenerator
    {

        private static char[][] numberGroups;

        static PhoneNumberGenerator()
        {
            numberGroups = new char[10][];
            numberGroups[0] = new char[] { '0' };
            numberGroups[1] = new char[] { '1', };
            numberGroups[2] = new char[] { '2', 'A', 'B', 'C', };
            numberGroups[3] = new char[] { '3', 'D', 'E', 'F', };
            numberGroups[4] = new char[] { '4', 'G', 'H', 'I', };
            numberGroups[5] = new char[] { '5', 'J', 'K', 'L', };
            numberGroups[6] = new char[] { '6', 'M', 'N', 'O', };
            numberGroups[7] = new char[] { '7', 'P', 'Q', 'R', };
            numberGroups[8] = new char[] { '8', 'S', 'T', 'U', };
            numberGroups[9] = new char[] { '9', 'W', 'X', 'Y', 'Z' };
        }

        private static IEnumerable<int> GetCombination(char[][] phoneNumberGroups, int k)
        {
            int rem;
            var l = phoneNumberGroups.Length - 1;
            var div = k;

            //modulo reduce k by the group sizes get the group selections
            for (int i = 0; i < phoneNumberGroups.Length; i++)
            {
                div = Math.DivRem(div, phoneNumberGroups[l - i].Length, out rem);
                yield return rem;
            }
        }

        public static IEnumerable<string> GenerateNumbers(string phoneNumber)
        {
            var phoneNumberGroups = phoneNumber.Where(d => d >= '0' && d <= '9').Select(c => numberGroups[c - '0']).ToArray();

            var groupIndexes = Enumerable.Repeat(0, phoneNumberGroups.Length).ToArray(); //Initial Variation of Number representation

            var numVariants = phoneNumberGroups.Select(ng => ng.Length).Aggregate(1, (n, acc) => acc * n); //multiply groups sizes together

            int i = 0;
            while (i < numVariants)
            {
                var nextCombo = GetCombination(phoneNumberGroups, i).ToArray().Reverse().Select((n, j) => phoneNumberGroups[j][n]).ToArray();
                var phoneNumberChars = phoneNumber.ToCharArray();
                var d = 0;
                for (int n = 0; n < phoneNumberChars.Length; n++)
                {
                    var c = phoneNumberChars[n];
                    if (c >= '0' && c <= '9')
                        phoneNumberChars[n] = nextCombo[d++];
                }

                var nextPhone = new String(phoneNumberChars);
                i++;
                yield return nextPhone;
            }
        }

        public static int NumVariations(string phoneNumber)
        {
            var groupSizes = phoneNumber.Where(d => d >= '0' && d <= '9').Select(c => numberGroups[c - '0'].Length).ToArray();
            var numCombinations = groupSizes.Aggregate(1, (acc, i) => i * acc);
            return numCombinations;
        }

        public static int NumPages(string phoneNumber, int pageSize)
        {
            var numCombinations = NumVariations(phoneNumber);
            var numPages = (int)Math.Ceiling(((double)numCombinations) / pageSize);
            
            return numPages;
        }
             

    }
}
