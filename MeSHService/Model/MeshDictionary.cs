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
        private StringTransformation UnifyTerm;

        private IDictionary<string, MeshDescriptor> _descriptorsByNumbers = new Dictionary<string, MeshDescriptor>();

        private IDictionary<string, MeshDescriptor> _descriptorsByTerms = new Dictionary<string, MeshDescriptor>();
        
        private IDictionary<int, IDictionary<string, MeshDescriptor>> _dictionariesByWordsCount =
            new Dictionary<int, IDictionary<string, MeshDescriptor>>();
        
        /// <param name="unifyingFunc">
        ///     Method creating unified form of words in string (expected normalization and stemming)
        /// </param>
        public MeshDictionary(StringTransformation unifyingFunc)
        {
            UnifyTerm = unifyingFunc;
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

        #region Matching

        public bool MatchWithExactTerm(string unifiedString, out MeshDescriptor matchingItem)
        {
            int wordsCount = unifiedString.Split().Length;
            if (_dictionariesByWordsCount.ContainsKey(wordsCount)
                && _dictionariesByWordsCount[wordsCount].ContainsKey(unifiedString))
            {
                matchingItem = _dictionariesByWordsCount[wordsCount][unifiedString];
                return true;
            }

            matchingItem = null;
            return false;
        }

        #endregion

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
                string unifiedTerm = UnifyTerm(term);
                
                if (!_descriptorsByTerms.ContainsKey(unifiedTerm))
                {
                    _descriptorsByTerms.Add(unifiedTerm, newDesc);
                    AddToDictionariesAccordingToWordsCount(newDesc, unifiedTerm);
                }
            }
        }

        private void AddToDictionariesAccordingToWordsCount(MeshDescriptor newDesc, string unifiedTerm)
        {
            int wordsCount = unifiedTerm.Split().Length;
            if (_dictionariesByWordsCount.ContainsKey(wordsCount))
            {
                _dictionariesByWordsCount[wordsCount].Add(unifiedTerm, newDesc);
            }
            else
            {
                _dictionariesByWordsCount.Add
                    (
                        wordsCount,
                        new Dictionary<string, MeshDescriptor>
                        {
                            { unifiedTerm, newDesc }
                        }
                    );
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
