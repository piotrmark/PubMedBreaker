using System.Collections.Generic;


namespace PubMedService
{
    public static class PubMedQueryHandler
    {
        public static List<PubMedQueryResult> GetResultsForPubMedQuery(string query)
        {
            return new List<PubMedQueryResult> { new PubMedQueryResult() };
        }
    }
}
