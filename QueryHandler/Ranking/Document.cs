using QueryHandler.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeSHService.Helpers.StringExtensions;

namespace QueryHandler.Ranking
{
    public class Document
    {
        private IDictionary<RankedTerm, double> TFxIDFbyTerm;

        public Document(string text, QueryTermsHandler termsHandler)
        {
            IList<Engine.Term> terms = termsHandler.GetDividedToTerms(text);
            Terms = terms.Select(t => t.ToComparable(termsHandler.UnifyTerm)).ToList();
            TFxIDFbyTerm = new Dictionary<RankedTerm, double>();
        }

        public IList<ComparableTerm> Terms { get; private set; }

        public int TotalTermsCount
        {
            get { return Terms.Count(); }
        }

        public int GetCountOf(ComparableTerm term)
        {
            return Terms.Count(t => t.Equals(term));
        }

        public double GetTFxIDF(RankedTerm term)
        {
            if (!TFxIDFbyTerm.ContainsKey(term))
                TFxIDFbyTerm.Add(term, ComputeTFxIDF(term));
            return TFxIDFbyTerm[term];
        }

        private double ComputeTFxIDF(RankedTerm term)
        {
            double termFrequency = (double)GetCountOf(term.Term) / (double)TotalTermsCount;
            return termFrequency * term.InverseDocumentFrequency;
        }
    }
}
