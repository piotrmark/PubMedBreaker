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

        public ComparableTerm(Engine.Term standardTerm, StringTransformation unifyingFunc)
        {
            AllSynonymsUnified = new List<string>
            {
                unifyingFunc(standardTerm.TextValue)
            };

            AllSynonymsUnified.AddRange(standardTerm.Synonyms.Select(s => unifyingFunc(s)).ToList());
        }


        public List<string> AllSynonymsUnified { get; private set; }

        public override bool Equals(Object obj)
        {
            ComparableTerm other = obj as ComparableTerm;

            if (other != null)
                return this.AllSynonymsUnified.Any(s => other.AllSynonymsUnified.Any(s2 => s2.Equals(s)));

            return base.Equals(obj);
        }

    }
}
