using QueryHandler.Ranking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeSHService.Helpers.StringExtensions;

namespace QueryHandler.Engine
{
    public class Term
    {
        public string TextValue { get; private set; }
        public IList<string> Synonyms { get; private set; }
        
        public Term(string textVal, IList<string> synonyms)
        {
            TextValue = textVal;
            Synonyms = synonyms;
        }

        public ComparableTerm ToComparable(StringTransformation unifyingFunc)
        {
            return new ComparableTerm(this, unifyingFunc);
        }
    }
}
