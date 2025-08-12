using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeChallenges
{
    internal class SearchInsert
    {
        public int SearchInsertPosition(int[] nums, int target) {
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] >= target)
                {                    
                    return i;
                }
                if (i == nums.Length - 1)
                {
                    return nums.Length;
                }
            }
            return -1;
        }

        public int SearchInsertBinary(int[] nums, int target)
        {
            int index = Array.BinarySearch(nums, target);
            return index >= 0 ? index : ~index;
        }
    }
}
