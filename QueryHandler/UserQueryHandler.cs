using System.Collections.Generic;
using PubMedService;

namespace QueryHandler
{
    public static class UserQueryHandler
    {
        public static List<UserQueryResult> GetResultsForQuery(string query)
        {
            List<PubMedQueryResult> pmResults = PubMedQueryHandler.GetResultsForPubMedQuery(query);

            List<UserQueryResult> result = new List<UserQueryResult>();

            foreach (var pmResult in pmResults)
            {
                result.Add(new UserQueryResult { ArticleTitle = pmResult.Article.Title });
            }

            return result;
        }
    }
}
