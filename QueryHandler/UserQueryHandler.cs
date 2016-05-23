using System.Collections.Generic;
using System.Threading.Tasks;
using PubMedService;
using MeSHService.Service;
using System.Diagnostics;
using System.Linq;
using QueryHandler.TextUnifiers;
using QueryHandler.Engine;
using PubMedService.PubMedQueryBuilder;

namespace QueryHandler
{
    public class UserQueryHandler
    {
        private TermHandler termsHandler = new TermHandler(Unification.Unify);
        
        public async Task<FinalResultsSet> GetResultsForQuery(string query, int resultsNumber, int timeout)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var multiQueryBuilder = new QueriesSetBuilder(termsHandler, query, resultsNumber);
            IList<IPubMedQueryBuilder> queries = multiQueryBuilder.GetQueriesToSend();

            var synonyms = termsHandler.GetSynonyms(query);

            List<UserQueryResult> result = new List<UserQueryResult>();

            foreach (IPubMedQueryBuilder queryToBuild in queries)
            {
                List<PubMedQueryResult> queryResults =
                    await PubMedQueryHandler.GetResultsForPubMedQueryAsync(queryToBuild);

                foreach (var queryRes in queryResults)
                {
                    int ranking = RankingHelper.GetRankingForResult(queryRes, queryResults);
                    result.Add(new UserQueryResult { ArticleTitle = queryRes.Article.Title, Ranking = ranking, ArticleId = queryRes.Article.PubMedId});
                }
            }

            FinalResultsSet frs = new FinalResultsSet();

            frs.UnifiedQuery = Unification.Unify(query);
            
            frs.UserQueryResults = result.OrderByDescending(r => r.Ranking).ToList();
            frs.ExecutionTimeMilis = stopwatch.ElapsedMilliseconds;
            frs.Synonyms = synonyms;

            return frs;
        }
    }
}
