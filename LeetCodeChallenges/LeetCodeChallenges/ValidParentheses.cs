using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeChallenges
{
    internal class ValidParentheses
    {
        public bool IsValid(string s)
        {
            var result_stack = new Stack<char>();
            var bracketsDict = new Dictionary<char, char> {
            { ')', '(' },
            { '}', '{' },
            { ']', '[' }
        };

            foreach (char ch in s)
            {
                if (bracketsDict.ContainsValue(ch))
                {
                    result_stack.Push(ch);
                }
                else if (bracketsDict.ContainsKey(ch))
                {
                    if (result_stack.Count == 0 || result_stack.Pop() != bracketsDict[ch])
                    {
                        return false;
                    }
                }
            }

            return result_stack.Count == 0;
        }
    }
}
