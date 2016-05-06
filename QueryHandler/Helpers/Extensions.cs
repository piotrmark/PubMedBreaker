using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryHandler
{
    public static class Extensions
    {
        public static string JoinRange(this List<string> words, int startPos, int length)
        {
            return string.Join(" ", words.GetRange(startPos, length));
        }

        public static string JoinRange(this List<string> words, int startPos)
        {
            return string.Join(" ", words.GetRange(startPos, words.Count - startPos));
        }
    }
}
