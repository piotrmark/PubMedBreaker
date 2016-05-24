using PubMed.Model.Abstract;
using PubMed.Model.Database;
using PubMed.Model.Summaries;
using PubMed.Search.Abstract;
using PubMed.Search.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubMedService
{
    public static class PubMedArticlesCache
    {
        private static IDictionary<string, PubMedArticle> articles =
            new Dictionary<string, PubMedArticle>();

        public async static Task<PubMedArticle> Get(string articleId, EntrezDatabase database)
        {
            if (!articles.ContainsKey(articleId))
                articles.Add(articleId, await Load(articleId, database));
            return articles[articleId];
        }

        private async static Task<PubMedArticle> Load(string articleId, EntrezDatabase database)
        {
            IPaperSummaryRetriever summaryRetriever = new PaperSummaryRetriever();
            var summaryRetProps = new SummaryRetrievalProperties(database, articleId);

            Summary summary =
                   await
                       summaryRetriever.RetrievePaperSummaryAsync(summaryRetProps);
            string title = summary.Title;

            IPaperAbstractRetriever abstractRetriever = new PaperAbstractRetriever();
            var abstractRetProps = new AbstractRetrievalProperties(database, articleId);

            string abstractTxt =
                   await
                       abstractRetriever.GetAbstractOfPaperAsync(abstractRetProps);

            return new PubMedArticle(articleId, title, abstractTxt);
        }
    }
}
