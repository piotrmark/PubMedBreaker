using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeSHService.Helpers.StringExtensions;

namespace MeSHService.Model
{
    public class MeshDictionary
    {
        /// <summary>
        /// Method creating unified form of words in string (expected normalization and stemming)
        /// </summary>
        private StringTransformation Unify;

        private IDictionary<string, MeshDescriptor> _descriptorsByNumbers = new Dictionary<string, MeshDescriptor>();

        private IDictionary<string, MeshDescriptor> _descriptorsByTerms = new Dictionary<string, MeshDescriptor>();

        private IDictionary<string, MeshDescriptor> _descriptorsByOneWordTerms = new Dictionary<string, MeshDescriptor>();
        private IDictionary<string, MeshDescriptor> _descriptorsByTwoWordTerms = new Dictionary<string, MeshDescriptor>();
        private IDictionary<string, MeshDescriptor> _descriptorsByThreeWordTerms = new Dictionary<string, MeshDescriptor>();
        private IDictionary<string, MeshDescriptor> _descriptorsByFourWordTerms = new Dictionary<string, MeshDescriptor>();
        private IDictionary<string, MeshDescriptor> _descriptorsByFiveWordTerms = new Dictionary<string, MeshDescriptor>();


        public MeshDictionary(StringTransformation unifyingFunc)
        {
            Unify = unifyingFunc;
        }
        
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
            foreach (string term in newDesc.Terms.Select(t => t.TextValue))
            {
                string key = Unify(term);
                int wordsCount = key.Split().Length;
                if (!_descriptorsByTerms.ContainsKey(key))
                {
                    _descriptorsByTerms.Add(key, newDesc);

                    switch(wordsCount)
                    {
                        case 1: _descriptorsByOneWordTerms.Add(key, newDesc); break;
                        case 2: _descriptorsByTwoWordTerms.Add(key, newDesc); break;
                        case 3: _descriptorsByThreeWordTerms.Add(key, newDesc); break;
                        case 4: _descriptorsByFourWordTerms.Add(key, newDesc); break;
                        case 5: _descriptorsByFiveWordTerms.Add(key, newDesc); break;
                        default: break;
                    }
                }
            }
        }

        private void AddToNumbersDict(MeshDescriptor newDesc)
        {
            foreach (string treeNum in newDesc.TreeIds)
                _descriptorsByNumbers.Add(treeNum, newDesc);
        }

        #endregion

    }
}
