using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeSHService.Model
{
    public class MeshDescriptor
    {
        private IList<string> _treeIds = new List<string>();
        private IList<string> _terms = new List<string>();

        public string Name { get; set; }

        public IList<string> TreeIds
        {
            get { return _treeIds; }
        }
        
        public IList<string> Terms
        {
            get { return _terms; }
        }
    }
}
