using System.Collections.Generic;
using System.Threading.Tasks;
using PubMed.Model.Database;
using PubMed.Model.Search;
using PubMed.Model.Search.Terms;
using PubMed.Model.Summaries;
using PubMed.Search.Search;
using PubMed.Search.Summary;
using PubMedService.PubMedQueryBuilder;

namespace PubMedService
{
    public static class PubMedQueryHandler
    {
        private static readonly EntrezDatabase EntrezDatabase = new EntrezDatabase(PubMedConsts.PubMedDatabaseName);

        public static async Task<List<PubMedQueryResult>> GetResultsForPubMedQueryAsync(
            IPubMedQueryBuilder searchBuilder)
        {
            var result = new List<PubMedQueryResult>();
            var searchProperties = searchBuilder.BuildSearch(EntrezDatabase);
            
            IDatabaseSearchExecutor databaseSearchExecutor = new DatabaseSearchExecutor();
            var searchResults = await databaseSearchExecutor.ExecuteSearchAsync(searchProperties);

            foreach (var searchResult in searchResults)
            {
                IPaperSummaryRetriever paperSummaryRetriever = new PaperSummaryRetriever();
                var summary =
                    await
                        paperSummaryRetriever.RetrievePaperSummaryAsync(new SummaryRetrievalProperties(EntrezDatabase,
                            searchResult.PubMedID));
                result.Add(new PubMedQueryResult(new PubMedArticle(summary.Title), searchResults.IndexOf(searchResult)));
            }

            return result;
        }
    }
}
