using MeSHService.Service;
using PubMedService.PubMedQueryBuilder;
using PubMedService.PubMedService.PubMedQueryBuilder;
using QueryHandler.TextUnifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using static MeSHService.Helpers.StringExtensions;

namespace QueryHandler.Engine
{
    public class QueriesSetBuilder
    {
        private TermHandler termsHandler;
        private string userQuery;
        private int maxResultsCount = 10;


        public QueriesSetBuilder(TermHandler termsHandler, string userQuery, int maxResultsCount)
        {
            this.termsHandler = termsHandler;
            this.userQuery = userQuery;
            this.maxResultsCount = maxResultsCount;
        }

        public IList<IPubMedQueryBuilder> GetQueriesToSend()
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
            List<Term> termsWithSynonyms = termsHandler.GetDividedToTerms(userQuery);
            List<IList<string>> synonymsList = termsWithSynonyms.Select(t => t.Synonyms).ToList();
            return new SynonymousQueryBuilder(synonymsList, maxResultsCount);
        }

        private IPubMedQueryBuilder GetAllSynonymsQueryBuilder(string userQuery)
        {
            IList<string> allSynonymsTogether = termsHandler.GetSynonyms(userQuery);
            return new SimpleAlternativeQueryBuilder(allSynonymsTogether, maxResultsCount);
        }
    }
}
