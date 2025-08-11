using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeChallenges
{
    internal class LongestCommonPrefix
    {
        public string LongestCommonPrefix1(string[] words) {
            string result = string.Empty;
            for (int i = 0; i < words.Length; i++)
            {
                if (i == 0)
                {
                    result = words[i];
                }
                else
                {
                    int j = 0;
                    while (j < result.Length && j < words[i].Length && result[j] == words[i][j])
                    {
                        j++;
                    }
                    result = result.Substring(0, j);
                }
            }
            return result;
        }

    }
}
