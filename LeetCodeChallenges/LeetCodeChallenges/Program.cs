namespace LeetCodeChallenges
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Test TwoSum
            TwoSum twoSum = new TwoSum();
            int[] nums = { 2, 3, 6, 15 };
            int target = 9;
            int[] result = twoSum.TwoSum_BruteForce(nums, target);
            Console.WriteLine("TwoSum Brute Force Result: [{0}, {1}]", result[0], result[1]);

            //Test TwoSum Optimized
            int[] resultOptimized = twoSum.TwoSum_Optimized(nums, target);
            Console.WriteLine("TwoSum Optimized Result: [{0}, {1}]", resultOptimized[0], resultOptimized[1]);

            //Test PalindromeNumber
            PalindromeNumber palindromeNumber = new PalindromeNumber();
            int num = 356;
            Console.WriteLine("Number {0} is a palindrome? Result: {1}",num, palindromeNumber.IsPalindrome(num)); // Should return true

            //Test PalindromeNumber Residuals
            int numResiduals = 14321;
            Console.WriteLine("Number {0} is a palindrome? Result: {1}", numResiduals, palindromeNumber.IsPalindrome_Residuals(numResiduals)); // Should return true

            //Test RomanToInteger
            RomanToInteger romanToInteger = new RomanToInteger();
            string roman = "MMXXV";
            int integerResult = romanToInteger.RomanToInt(roman);
            Console.WriteLine("Roman numeral {0} converts to integer: {1}", roman, integerResult); // Should return 1994

            //test longest common prefix
            LongestCommonPrefix longestCommonPrefix = new LongestCommonPrefix();
            string[] strs = { "flower", "flow", "flight" };
            string prefix = longestCommonPrefix.LongestCommonPrefix1(strs);
            Console.WriteLine("Longest common prefix of {0} is: {1}", string.Join(", ", strs), prefix); // Should return "fl"

        }
    }
}
