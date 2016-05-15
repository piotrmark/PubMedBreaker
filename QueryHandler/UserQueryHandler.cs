using System.Collections.Generic;
using System.Threading.Tasks;
using PubMedService;
using MeSHService.Service;
using System.Diagnostics;
using System.Linq;
using QueryHandler.TextUnifiers;

namespace QueryHandler
{
    public class UserQueryHandler
    {
        private readonly MeshService _meshService = new MeshService(Unification.Unify);
        
        public async Task<FinalResultsSet> GetResultsForQuery(string query, int resultsNumber, int timeout)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            List<string> terms = new List<string>();
            Dictionary<string, List<string>> termsSynonyms = new Dictionary<string, List<string>>();
            Dictionary<string, List<PubMedQueryResult>> termsResults = new Dictionary<string, List<PubMedQueryResult>>();
            Dictionary<string, List<PubMedQueryResult>> synonymsResults =
                new Dictionary<string, List<PubMedQueryResult>>();

            /*List<PubMedQueryResult> wholeQueryResults =
                    await PubMedQueryHandler.GetResultsForPubMedQueryAsync(query, resultsNumber);*/

            terms.Add(query);   //TODO: W tym miejscu powinniśmy wyciągać listę termów z MeSHa

            foreach (var term in terms)
            {
                List<PubMedQueryResult> termResults =
                    await PubMedQueryHandler.GetResultsForPubMedQueryAsync(term, resultsNumber);
                termsResults.Add(term, termResults);
                IList<string> synonyms = _meshService.GetSynonyms(term);
                termsSynonyms.Add(term, synonyms.ToList());
            }

            int maxSynonymsNumber = termsSynonyms.Max(ts => ts.Value.Count);

            bool isTimeOut = false;

            FinalResultsSet frs = new FinalResultsSet {SynonymsCount = maxSynonymsNumber};

            frs.UnifiedQuery = Unification.Unify(query);

            for (int synonymIndex = 0; synonymIndex < maxSynonymsNumber; synonymIndex++)
            {
                if (isTimeOut)
                {
                    frs.ProcesedSynonymsCount = synonymIndex;
                    break;
                }
                foreach (var termSynonyms in termsSynonyms)
                {
                    if (stopwatch.ElapsedMilliseconds > timeout * 1000)
                    {
                        isTimeOut = true;
                        break;
                    }
                    string synonym = termSynonyms.Value[synonymIndex];
                    List<PubMedQueryResult> synonymResults =
                        await PubMedQueryHandler.GetResultsForPubMedQueryAsync(synonym, resultsNumber);
                    synonymsResults.Add(synonym, synonymResults);
                }
            }
            List<UserQueryResult> result = new List<UserQueryResult>();

            foreach (var termResults in termsResults)
            {
                foreach (var termResult in termResults.Value)
                {
                    int ranking = RankingHelper.GetRankingForResult(termResult, termResults.Key, termsResults);
                    result.Add(new UserQueryResult { ArticleTitle = termResult.Article.Title, Ranking = ranking });
                }
            }

            foreach (var synonymResults in synonymsResults)
            {
                foreach (var synonymResult in synonymResults.Value)
                {
                    int ranking = RankingHelper.GetRankingForResult(synonymResult, synonymResults.Key, synonymsResults);    //TODO: concat?
                    result.Add(new UserQueryResult { ArticleTitle = synonymResult.Article.Title, Ranking = ranking });
                }
            }

            
            frs.UserQueryResults = result.OrderByDescending(r => r.Ranking).ToList();
            frs.ExecutionTimeMilis = stopwatch.ElapsedMilliseconds;

            return frs;
        }
    }
}
