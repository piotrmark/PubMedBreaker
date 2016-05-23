using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeSHService.Helpers.StringExtensions;

namespace QueryHandler.Ranking
{
    public class ComparableTerm 
    {
        private List<string> allSynonymsUnified;

        public ComparableTerm(Engine.Term standardTerm, StringTransformation unifyingFunc) 
        {
            allSynonymsUnified = new List<string>
            {
                unifyingFunc(standardTerm.TextValue)
            };

            allSynonymsUnified.AddRange(standardTerm.Synonyms.Select(s => unifyingFunc(s)).ToList());
        }

        public override bool Equals(Object obj)
        {
            ComparableTerm other = obj as ComparableTerm;

            if (other != null)
                return this.allSynonymsUnified.Any(s => other.allSynonymsUnified.Any(s2 => s2.Equals(s)));

            return base.Equals(obj);
        }
    }
}
