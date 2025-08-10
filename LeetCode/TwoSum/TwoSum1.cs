public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        Dictionary<int, int> map = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++){ 
            map[nums[i]] = i;
        }

        for (int i = 0; i < nums.Length; i++) {
            int complement = target - nums[i];
            if (map.ContainsKey(complement) && map[complement] != i) {
                return new int[] {i, map[complement]};
            }
        }

        return new int[] {};
    }

    int[] nums = {2,11,15,7};
    int target = 9;

    Console.WriteLine("Solution: " + TwoSum(nums, target));


}