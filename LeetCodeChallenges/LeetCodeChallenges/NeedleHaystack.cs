using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeChallenges
{
    internal class NeedleHaystack
    {
        public int StrStr(string haystack, string needle)
        {
            bool b = haystack.Contains(needle);
            if (b)
            {
                return haystack.IndexOf(needle);
            }
            else
            {
                return -1;
            }
        }
    }
}
