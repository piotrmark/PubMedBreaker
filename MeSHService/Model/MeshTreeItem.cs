using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeSHService.Model
{
    public class MeshTreeItem
    {
        private string _treeNumber;
        private List<MeshTreeItem> _children;

        public MeshTreeItem(string treeNum)
        {
            _treeNumber = treeNum;
            _children = new List<MeshTreeItem>();
        }


        public List<MeshTreeItem> Children
        {
            get { return _children; }
        }

        public string ParentNumber()
        {
            return ParentNumber(_treeNumber);
        }

        public static string ParentNumber(string treeNum)
        {
            int lastDot = treeNum.LastIndexOf('.');
            return lastDot == -1
                ? string.Empty
                : treeNum.Substring(0, lastDot);
        }
    }
}
