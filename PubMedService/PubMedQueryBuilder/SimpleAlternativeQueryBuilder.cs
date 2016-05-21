using PubMed.Model.Search;
using PubMed.Model.Search.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubMedService.PubMedQueryBuilder
{
    public class SimpleAlternativeQueryBuilder : AbstractQueryBuilder
    {
        private IList<string> queryTerms;

        public SimpleAlternativeQueryBuilder(IList<string> queryTerms, int maxResults) : base (maxResults)
        {
            this.queryTerms = queryTerms;
        }

        protected override SearchTermGroup BuildBaseTermGroup()
        {
            var mainGroup = new SearchTermGroup()
            {
                GroupLinkType = new SearchTermLinkType(LinkTypes.OR)
            };

            foreach (string term in queryTerms)
                mainGroup.AddTerm<AllFieldsTerm>(term, LinkTypes.OR);
            return mainGroup;
        }
    }
}
