using System;
using System.Text.RegularExpressions;

namespace QueryHandler.TextUnifiers
{
    public static class Normalization
    {
        public static string Normalize(string text)
        {
            string result = text.ToLower();
            result = result.RemoveSpecialCharacters();
            result = result.RemoveMultipleSpaces();
            return result;

        }

        private static string RemoveSpecialCharacters(this string text)
        {
            string regEx = "[a-zA-Z0-9 ]";
            string result = String.Empty;
            foreach (var character in text)
            {
                if (Regex.IsMatch(character.ToString(), regEx))
                {
                    result += character;
                }
                else
                {
                    result += " ";
                }
            }
            return result;
        }

        private static string RemoveMultipleSpaces(this string text)
        {
            string doubleSpace = "  ";
            string space = " ";
            while (text.Contains(doubleSpace))
            {
                text = text.Replace(doubleSpace, space);
            }
            return text;
        }
    }
}
