

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

        public PubMedArticle(string id)
        {
            PubMedId = id;
        }
        
        public async Task Load(EntrezDatabase database)
        {
            IPaperSummaryRetriever summaryRetriever = new PaperSummaryRetriever();
            var summaryRetProps = new SummaryRetrievalProperties(database, PubMedId);

            Summary summary =
                   await
                       summaryRetriever.RetrievePaperSummaryAsync(summaryRetProps);
            Title = summary.Title;

            IPaperAbstractRetriever abstractRetriever = new PaperAbstractRetriever();
            var abstractRetProps = new AbstractRetrievalProperties(database, PubMedId);

            string Abstract =
                   await
                       abstractRetriever.GetAbstractOfPaperAsync(abstractRetProps);
        }
    }
}
