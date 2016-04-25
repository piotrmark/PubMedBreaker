using System.Collections.Generic;
using System.Threading.Tasks;
using PubMed.Model.Database;
using PubMed.Model.Search;
using PubMed.Model.Search.Terms;
using PubMed.Model.Summaries;
using PubMed.Search.Search;
using PubMed.Search.Summary;


namespace PubMedService
{
    public static class PubMedQueryHandler
    {
        private static readonly EntrezDatabase EntrezDatabase = new EntrezDatabase(PubMedConsts.PubMedDatabaseName);

        public static async Task<List<PubMedQueryResult>> GetResultsForPubMedQueryAsync(string query, int resultsNumber)
        {
            var result = new List<PubMedQueryResult>();
            var searchProperties = BuildSearchProperties(query, resultsNumber);
            
            IDatabaseSearchExecutor databaseSearchExecutor = new DatabaseSearchExecutor();
            var searchResults = await databaseSearchExecutor.ExecuteSearchAsync(searchProperties);
            var count = searchResults.Count;

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

        private static SearchProperties BuildSearchProperties(string query, int resultsNumber)
        {
            var searchProperties = new SearchProperties
            {
                Database = EntrezDatabase,
                MaximumResults = resultsNumber,
                BaseSearchTermGroup = BuildSearch(query)
            };
            return searchProperties;
        }

        private static SearchTermGroup BuildSearch(string query)
        {
            var baseGroup = new SearchTermGroup();
            baseGroup.AddTerm<AllFieldsTerm>(query, LinkTypes.AND);
            return baseGroup;
        }
    }
}
