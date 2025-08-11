using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeChallenges
{
    internal class PalindromeNumber
    {
        public bool IsPalindrome(int x)
        {
            // Negative numbers are not palindromes
            if (x < 0) return false;
            // Convert the number to a string
            string str = x.ToString();
            // Check if the string is equal to its reverse
            return str == new string(str.Reverse().ToArray());
        }

        public bool IsPalindrome_Residuals(int x)
        {
            //Negative numbers are not palindromes
            if (x < 0) return false;
            // Single digit numbers are always palindromes
            if (x < 10) return true;
            
            int temp = x;
            int reversed = 0;
            while (temp > 0)
            {
                // Get the last digit
                int lastDigit = temp % 10;
                // Build the reversed number
                reversed = reversed * 10 + lastDigit;
                // Remove the last digit from temp
                temp /= 10;
            }
            // Check if the original number is equal to the reversed number
            return x == reversed;

        }
    }
}
