using MeSHService.Service;
using PubMedService.PubMedQueryBuilder;
using PubMedService.PubMedService.PubMedQueryBuilder;
using QueryHandler.TextUnifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeSHService.Helpers.StringExtensions;

namespace QueryHandler.Engine
{
    public class QueryProcessor
    {
        private MeshService service;
        private StringTransformation unifyTerm = (string source) => { return Unification.Unify(source); };
        private int maxResultsCount = 10;

        public QueryProcessor()
        {
            service = new MeshService(unifyTerm);
        }

        public IList<IPubMedQueryBuilder> GetQueriesToSend(string userQuery, int maxResultsCount)
        {
            return new List<IPubMedQueryBuilder>
            {
                GetOriginalQueryBuilder(userQuery),
                GetSynonymBasedQueryBuilder(userQuery),
                GetAllSynonymsQueryBuilder(userQuery)
            };
        }

        private IPubMedQueryBuilder GetOriginalQueryBuilder(string userQuery)
        {
            return new SimpleConjunctionQueryBuilder(userQuery.Split().ToList(), maxResultsCount);
        }


        private IPubMedQueryBuilder GetSynonymBasedQueryBuilder(string userQuery)
        {
            List<Tuple<string, IList<string>>> termsWithSynonyms = ReplaceWithSynonyms(userQuery);
            List<IList<string>> synonymsList = termsWithSynonyms.Select(t => t.Item2).ToList();
            return new SynonymousQueryBuilder(synonymsList, maxResultsCount);
        }

        private IPubMedQueryBuilder GetAllSynonymsQueryBuilder(string userQuery)
        {
            List<Tuple<string, IList<string>>> termsWithSynonyms = ReplaceWithSynonyms(userQuery);

            IList<string> allSynonymsTogether = new List<string>();
            foreach (IList<string> synonyms in termsWithSynonyms.Select(t => t.Item2))
                allSynonymsTogether = allSynonymsTogether.Concat(synonyms).ToList();

            return new SimpleAlternativeQueryBuilder(allSynonymsTogether, maxResultsCount);
        }

        private List<Tuple<string, IList<string>>> ReplaceWithSynonyms(string userQuery)
        {
            var result = new List<Tuple<string, IList<string>>>();

            var words = userQuery.Split().ToList();
            for (int termLength = words.Count; termLength > 1; termLength--)
            {
                for (int i = 0; i + termLength <= words.Count; i++)
                {
                    string currentlyChecked = unifyTerm(words.JoinRange(i, termLength));
                    IList<string> synonyms = service.GetExactSynonyms(currentlyChecked);
                    if(synonyms.Any())
                    {
                        if (i > 0)
                            result = ReplaceWithSynonyms(words.JoinRange(0, i));

                        result.Add(new Tuple<string, IList<string>>(currentlyChecked, synonyms));

                        if (i + termLength < words.Count)
                            result.AddRange(ReplaceWithSynonyms(words.JoinRange(i + termLength)));

                        return result;
                    }
                }
            }

            foreach(string word in words)
                result.Add(new Tuple<string, IList<string>>(word, service.GetSynonyms(word)));

            return result;
        }
    }
}
