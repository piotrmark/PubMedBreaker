

namespace PubMedService
{
    public class PubMedArticle
    {
        public string Title { get; private set; }
        public string PubMedId { get; }

        public PubMedArticle(string id, string title)
        {
            PubMedId = id;
            Title = title;
        }
    }
}
