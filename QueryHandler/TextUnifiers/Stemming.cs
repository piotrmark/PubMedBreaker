using System;
using System.Collections.Generic;
using QueryHandler.TextUnifiers.Stemmers;

namespace QueryHandler.TextUnifiers
{
    public static class Stemming
    {
        public static string Stem(string text)
        {
            SnowballStemmer stemmer = new SnowballStemmer();
            var splitted = text.Split();
            List<String> result = new List<string>();
            foreach (var word in splitted)
            {
                result.Add(stemmer.Stem(word));
            }
            return string.Join(" ", result);
        }
        
    }
}
