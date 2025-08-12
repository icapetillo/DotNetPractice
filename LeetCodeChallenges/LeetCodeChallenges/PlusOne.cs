using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeChallenges
{
    internal class PlusOne
    {
        public int[] GetPlusOne(int[] digits)
        {
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                if (digits[i] < 9)
                {
                    digits[i]++;
                    return digits;
                }
                digits[i] = 0;
            }

            int[] result = new int[digits.Length + 1];
            result[0] = 1;
            return result;

            ////Create number from array
            //string numberString = string.Join("", digits);
            //long number = long.Parse(numberString);

            ////Add one to number
            //number += 1;

            ////Convert back to array
            //string newNumberString = number.ToString();
            //int[] result = newNumberString.Select(c => int.Parse(c.ToString())).ToArray();

            //return result;
        }
    }
}
