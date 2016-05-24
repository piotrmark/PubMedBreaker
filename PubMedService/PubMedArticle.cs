

using PubMed.Model.Abstract;
using PubMed.Model.Database;
using PubMed.Model.Summaries;
using PubMed.Search.Abstract;
using PubMed.Search.Summary;
using System.Threading.Tasks;

namespace PubMedService
{
    public class PubMedArticle
    {
        public string Title { get; private set; }
        public string Abstract { get; private set; }
        public string PubMedId { get; private set; }

        public PubMedArticle(string id, string title, string abstractTxt)
        {
            PubMedId = id;
            Title = title;
            Abstract = abstractTxt;
        }
    }
}
