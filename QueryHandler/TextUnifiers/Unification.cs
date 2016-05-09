
namespace QueryHandler.TextUnifiers
{
    public static class Unification
    {
        public static string Unify(string text)
        {
            string normalized = Normalization.Normalize(text);
            return Stemming.Stem(normalized);
        }
    }
}
