using MeSHService.Model;
using MeSHService.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeSHService.Helpers.StringExtensions;

namespace MeSHService.Service
{
    /// <summary>
    /// Provides interface for main search engine module
    /// </summary>
    public class MeshService
    {
        MeshDictionary _dictionary;
        
        public MeshService(StringTransformation unifyingAlgorithm)
        {
            var dictBuilder = new MeshDictionaryBuilder();
            DescriptorRecordSet xmlResult = XmlMeshReader.Read();
            _dictionary = dictBuilder.Build(xmlResult, unifyingAlgorithm);
        }

        public IList<string> GetSynonyms(string unifiedWord)
        {
            MeshDescriptor matchingDescriptor = null;

            if (_dictionary.DescriptorsByTerms.ContainsKey(unifiedWord))
            {
                matchingDescriptor = _dictionary.DescriptorsByTerms[unifiedWord];
            }
            else
            {
                matchingDescriptor = _dictionary.DescriptorsByTerms.FirstOrDefault(
                    desc =>
                        desc.Key.Contains(unifiedWord)).Value;
            }

            return matchingDescriptor != null
                ? matchingDescriptor.Terms
                    .Where(t => !t.IsPermutedTerm)
                    .Select(t => t.TextValue)
                    .ToList()
                : new List<string> { unifiedWord };
        }

        public IList<string> GetExactSynonyms(string unifiedString)
        {
            MeshDescriptor matchingItem;

            if (_dictionary.MatchWithExactTerm(unifiedString, out matchingItem))
            {
                return matchingItem.Terms
                    .Where(term => !term.IsPermutedTerm)
                    .Select(term => term.TextValue)
                    .ToList();
            }

            return new List<string>();
        }
    }
}
