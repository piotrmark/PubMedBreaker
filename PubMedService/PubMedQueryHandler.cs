using System.Collections.Generic;
using System.Threading.Tasks;
using PubMed.Model.Database;
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
            var foundArticles = new List<PubMedQueryResult>();
            var searchProperties = searchBuilder.BuildSearch(EntrezDatabase);
            
            IDatabaseSearchExecutor databaseSearchExecutor = new DatabaseSearchExecutor();
            var searchResults = await databaseSearchExecutor.ExecuteSearchAsync(searchProperties);

            foreach (var searchResult in searchResults)
            {
                var resultArticle = new PubMedArticle(searchResult.PubMedID);
                await resultArticle.Load(EntrezDatabase);
                var result = new PubMedQueryResult(resultArticle, searchResults.IndexOf(searchResult));
                foundArticles.Add(result);
            }

            return foundArticles;
        }
    }
}
