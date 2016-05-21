using PubMed.Model.Database;
using PubMed.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubMedService.PubMedQueryBuilder
{
    public interface IPubMedQueryBuilder
    {
        SearchProperties BuildSearch(EntrezDatabase searchedDb);
    }
}
