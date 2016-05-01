using MeSHService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeSHService.Builders
{
    /// <summary>
    /// Constructs mesh tree. For future use (sic!)
    /// </summary>
    class MeshTreeBuilder
    {
        private void AddToTree(MeshDescriptor newDesc)
        {
            foreach (string treeNum in newDesc.TreeIds)
            {
                
            }
        }
    }
}
