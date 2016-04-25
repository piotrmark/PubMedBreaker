

namespace PubMedService
{
    public class PubMedQueryResult
    {
        public PubMedArticle Article { get; private set; }
        public int PubMedIndex { get; set; }

        public PubMedQueryResult(PubMedArticle article, int pubMedIndex)
        {
            Article = article;
            PubMedIndex = pubMedIndex;
        }
    }
}
