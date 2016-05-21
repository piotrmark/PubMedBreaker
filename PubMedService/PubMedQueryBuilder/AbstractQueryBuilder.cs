using PubMed.Model.Database;
using PubMed.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubMedService.PubMedQueryBuilder
{
    public abstract class AbstractQueryBuilder : IPubMedQueryBuilder
    {
        private int maxResults;

        public AbstractQueryBuilder(int maxResults)
        {
            this.maxResults = maxResults;
        }

        protected abstract SearchTermGroup BuildBaseTermGroup();

        public SearchProperties BuildSearch(EntrezDatabase searchedDb)
        {
            return new SearchProperties
            {
                Database = searchedDb,
                BaseSearchTermGroup = BuildBaseTermGroup(),
                MaximumResults = maxResults
            };
        }
    }
}
