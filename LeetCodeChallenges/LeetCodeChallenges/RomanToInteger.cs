using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeChallenges
{
    internal class RomanToInteger
    {
        public int RomanToInt(string s)
        {
            // Dictionary to hold the Roman numeral values
            Dictionary<char, int> romanValues = new Dictionary<char, int>
            {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000}
            };
            int total = 0;
            int prevValue = 0;
            // Iterate through each character in the string
            for (int i = s.Length - 1; i >= 0; i--)
            {
                char currentChar = s[i];
                int currentValue = romanValues[currentChar];
                // If the current value is less than the previous value, subtract it
                if (currentValue < prevValue)
                {
                    total -= currentValue;
                }
                else
                {
                    // Otherwise, add it to the total
                    total += currentValue;
                }
                // Update the previous value
                prevValue = currentValue;
            }
            // Return the total value
            return total;
        }
    }
}
