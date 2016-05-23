using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeSHService.Helpers.StringExtensions;

namespace QueryHandler.Engine
{
    public class ComparableTerm : Term
    {
        private List<string> allSynonymsUnified;

        public ComparableTerm(Term standardTerm, StringTransformation unifyingFunc) 
            : base(standardTerm.TextValue, standardTerm.Synonyms)
        {
            allSynonymsUnified = new List<string>
            {
                unifyingFunc(TextValue)
            };

            allSynonymsUnified.AddRange(Synonyms.Select(s => unifyingFunc(s)).ToList());
        }

        public override bool Equals(Object obj)
        {
            ComparableTerm other = obj as ComparableTerm;

            if (other != null)
            {
                return this.allSynonymsUnified.Any(s => other.allSynonymsUnified.Any(s2 => s2.Equals(s)));
            }

            return base.Equals(obj);
        }
    }
}
