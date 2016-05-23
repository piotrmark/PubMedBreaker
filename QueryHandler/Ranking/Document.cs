﻿using QueryHandler.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryHandler.Ranking
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDocument
    {
        int TotalTermsCount { get; }
        int GetCountOf(ComparableTerm term);
    }
}
