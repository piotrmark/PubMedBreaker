

namespace PubMedService
{
    public class PubMedArticle
    {
        public string Title { get; set; }
        public string Abstract { get; set; }

        public PubMedArticle(string title)
        {
            Title = title;
        }
    }
}
