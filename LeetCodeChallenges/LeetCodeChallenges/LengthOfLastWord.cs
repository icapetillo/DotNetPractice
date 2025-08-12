using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeChallenges
{
    internal class LengthOfLastWord
    {
        public int GetLengthOfLastWord(string phrase) {
            string[] words = phrase.Trim().Split(' ');
            if (words.Length > 0){
                return words[words.Length - 1].Length;
            }
            return 0;
        }
    }
}
