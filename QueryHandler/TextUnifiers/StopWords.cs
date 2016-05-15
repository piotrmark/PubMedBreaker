using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace QueryHandler.TextUnifiers
{
    public static class StopWords
    {
        private static List<String> _wordsList;

        private static void ReadFromFile()
        {
            string path = "StopWords.txt";
            _wordsList = File.ReadAllLines(path).ToList();
        }

        public static String Clean(string text)
        {
            if (_wordsList == null)
            {
                ReadFromFile();
            }
            var splitted = text.Split();
            var result = new List<String>();
            foreach (var word in splitted)
            {
                Debug.Assert(_wordsList != null, "_wordsList != null");
                if (!_wordsList.Contains(word))
                {
                    result.Add(word);
                }
            }
            return string.Join(" ", result);
        }
    }
}
