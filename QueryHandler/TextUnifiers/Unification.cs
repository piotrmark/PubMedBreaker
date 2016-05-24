
namespace QueryHandler.TextUnifiers
{
    public static class Unification
    {
        public static string Unify(string text)
        {
            string normalized = Normalization.Normalize(text);
            string cleaned = StopWords.Clean(normalized);
            string stemmed = Stemming.Stem(cleaned);
            return stemmed.Trim();
        }
    }
}
