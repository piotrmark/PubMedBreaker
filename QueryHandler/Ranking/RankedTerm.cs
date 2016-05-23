using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryHandler.Ranking
{
    public class RankedTerm
    {
        public ComparableTerm Term { get; set; }
        public double InverseDocumentFrequency { get; set; }
    }
}
