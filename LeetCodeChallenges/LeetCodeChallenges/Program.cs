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

            //Test Valid Parentheses
            ValidParentheses validParentheses = new ValidParentheses();
            string parentheses = "({[]})";
            bool isValid = validParentheses.IsValid(parentheses);
            Console.WriteLine("The parentheses string '{0}' is valid: {1}", parentheses, isValid); // Should return true

            //Test Happy Number
            HappyNumber happyNumber = new HappyNumber();
            int happyNum = 2;
            bool isHappy = happyNumber.isHappyNumber(happyNum);
            Console.WriteLine("The number {0} is a happy number: {1}", happyNum, isHappy); // Should return true

            //Test NeedleHaystack
            NeedleHaystack needleHaystack = new NeedleHaystack();
            string haystack = "butsadbut";
            string needle = "rrr";
            int index = needleHaystack.StrStr(haystack, needle);
            Console.WriteLine("The index of the needle '{0}' in the haystack '{1}' is: {2}", needle, haystack, index); // Should return 2

            //Test SearchInsert
            SearchInsert searchInsert = new SearchInsert();
            int[] numsSearchInsert = { 1, 3, 5, 6 };
            int targetSearchInsert = 7;
            int insertIndex = searchInsert.SearchInsertPosition(numsSearchInsert, targetSearchInsert);
            Console.WriteLine("The target {0} should be inserted at index: {1}", targetSearchInsert, insertIndex); // Should return 2
            insertIndex = searchInsert.SearchInsertBinary(numsSearchInsert, targetSearchInsert);
            Console.WriteLine("The target {0} should be inserted at index (binary search): {1}", targetSearchInsert, insertIndex); // Should return 2
        }
    }
}
