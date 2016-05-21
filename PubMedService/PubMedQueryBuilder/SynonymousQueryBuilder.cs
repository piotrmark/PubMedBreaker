using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubMed.Model.Search;
using PubMed.Model.Search.Terms;

namespace PubMedService.PubMedQueryBuilder
{
    public class SynonymousQueryBuilder : AbstractQueryBuilder
    {
        private IList<IList<string>> synonymsList;

        public SynonymousQueryBuilder(IList<IList<string>> synonymsList, int maxResults) :
            base(maxResults)
        {
            this.synonymsList = synonymsList;
        }

        protected override SearchTermGroup BuildBaseTermGroup()
        {
            var mainTermsGroup = new SearchTermGroup();

            foreach(IList<string> termSynonms in synonymsList)
            {
                var termGroup = new SearchTermGroup();
                foreach(string synonym in termSynonms)
                    termGroup.AddTerm<AllFieldsTerm>(synonym, LinkTypes.OR);

                mainTermsGroup.Children.Add(termGroup);
            }

            return mainTermsGroup;
        }
    }
}
