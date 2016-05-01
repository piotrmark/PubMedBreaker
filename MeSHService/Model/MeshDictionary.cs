using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeSHService.Model
{
    public class MeshDictionary
    {
        private IDictionary<string, MeshDescriptor> _descriptorsByNumbers = new Dictionary<string, MeshDescriptor>();
        private IDictionary<string, MeshDescriptor> _descriptorsByTerms = new Dictionary<string, MeshDescriptor>();
        
        
        public IDictionary<string, MeshDescriptor> DescriptorsByNumbers
        {
            get
            {
                return _descriptorsByNumbers;
            }
        }

        public IDictionary<string, MeshDescriptor> DescriptorsByTerms
        {
            get
            {
                return _descriptorsByTerms;
            }
        }

        #region Importing new item

        public void Add(MeshDescriptor newDesc)
        {
            AddToTermsDict(newDesc);
            AddToNumbersDict(newDesc);
            
        }

        private void AddToTermsDict(MeshDescriptor newDesc)
        {
            foreach (string term in newDesc.Terms)
                _descriptorsByTerms.Add(term, newDesc);
        }

        private void AddToNumbersDict(MeshDescriptor newDesc)
        {
            foreach (string treeNum in newDesc.TreeIds)
                _descriptorsByNumbers.Add(treeNum, newDesc);
        }

        

        #endregion

    }
}
