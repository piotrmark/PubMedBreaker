using System.Collections.Generic;
using System.Threading.Tasks;
using PubMedService;

namespace QueryHandler
{
    public static class UserQueryHandler
    {
        public static async Task<List<UserQueryResult>> GetResultsForQuery(string query, int resultsNumber)
        {
            List<PubMedQueryResult> pmResults = await PubMedQueryHandler.GetResultsForPubMedQueryAsync(query, resultsNumber);

            List<UserQueryResult> result = new List<UserQueryResult>();

            foreach (var pmResult in pmResults)
            {
                result.Add(new UserQueryResult { ArticleTitle = pmResult.Article.Title });
            }

            return result;
        }
    }
}
