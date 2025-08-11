using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeChallenges
{
    internal class HappyNumber
    {
        public bool isHappyNumber(int num) {
            HashSet<int> seenNumbers = new HashSet<int>();
            seenNumbers.Add(num);
            while (num != 1) {
                //calculate the sum of the squares of the digits
                int sum = 0;
                while (num > 0)
                {
                    int digit = num % 10;
                    sum += digit * digit;
                    num /= 10;
                }
                // If the sum is already seen, it's not a happy number
                if (seenNumbers.Contains(sum))
                {
                    return false;
                }
                // Add the new sum to the set and update num
                seenNumbers.Add(sum);
                num = sum;
            }
            return true;
        }
    }
}
