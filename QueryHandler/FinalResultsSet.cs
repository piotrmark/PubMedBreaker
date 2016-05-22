using System;
using System.Collections.Generic;

namespace QueryHandler
{
    public class FinalResultsSet
    {
        public List<UserQueryResult> UserQueryResults { get; set; }
        public long ExecutionTimeMilis { get; set; }
        public int SynonymsCount { get; set; }
        public int ProcesedSynonymsCount { get; set; }
        public string UnifiedQuery { get; set; }
        public List<String> Synonyms { get; set; }
    }
}
