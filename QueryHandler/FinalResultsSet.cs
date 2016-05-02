using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryHandler
{
    public class FinalResultsSet
    {
        public List<UserQueryResult> UserQueryResults { get; set; }
        public long ExecutionTimeMilis { get; set; }
        public int SynonymsCount { get; set; }
        public int ProcesedSynonymsCount { get; set; }
    }
}
