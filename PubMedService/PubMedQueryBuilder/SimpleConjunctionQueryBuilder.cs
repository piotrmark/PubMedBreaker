using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubMed.Model.Search;
using PubMed.Model.Search.Terms;
using PubMedService.PubMedQueryBuilder;

namespace PubMedService.PubMedService.PubMedQueryBuilder
{
    public class SimpleConjunctionQueryBuilder : AbstractQueryBuilder
    {
        private IList<string> queryTerms;

        public SimpleConjunctionQueryBuilder(IList<string> queryTerms, int maxResults) : base (maxResults)
        {
            this.queryTerms = queryTerms;
        }

        protected override SearchTermGroup BuildBaseTermGroup()
        {
            var mainGroup = new SearchTermGroup();
            foreach(string term in queryTerms)
                mainGroup.AddTerm<AllFieldsTerm>(term, LinkTypes.AND);
            return mainGroup;
        }
    }
}
