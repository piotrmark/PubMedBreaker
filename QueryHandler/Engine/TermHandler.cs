using MeSHService.Service;
using QueryHandler.TextUnifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeSHService.Helpers.StringExtensions;

namespace QueryHandler.Engine
{
    public class TermHandler
    {
        private MeshService service;
        private StringTransformation unifyTerm = (string source) => { return Unification.Unify(source); };

        public TermHandler(StringTransformation unifyFunc)
        {
            unifyTerm = unifyFunc;
            service = new MeshService(unifyFunc);
        }

        public List<string> GetSynonyms(string query)
        {
            List<Tuple<string, IList<string>>> termsWithSynonyms = ReplaceWithSynonyms(query);
            return (termsWithSynonyms.Select(t => t.Item2)).SelectMany(x => x).ToList();
        }

        public List<Tuple<string, IList<string>>> ReplaceWithSynonyms(string termsStr)
        {
            var result = new List<Tuple<string, IList<string>>>();

            var words = termsStr.Split().ToList();
            for (int termLength = words.Count; termLength > 1; termLength--)
            {
                for (int i = 0; i + termLength <= words.Count; i++)
                {
                    string currentlyChecked = unifyTerm(words.JoinRange(i, termLength));
                    IList<string> synonyms = service.GetExactSynonyms(currentlyChecked);
                    if (synonyms.Any())
                    {
                        if (i > 0)
                            result = ReplaceWithSynonyms(words.JoinRange(0, i));

                        result.Add(new Tuple<string, IList<string>>(currentlyChecked, synonyms));

                        if (i + termLength < words.Count)
                            result.AddRange(ReplaceWithSynonyms(words.JoinRange(i + termLength)));

                        return result;
                    }
                }
            }

            foreach (string word in words)
                result.Add(new Tuple<string, IList<string>>(word, service.GetSynonyms(word)));

            return result;
        }

    }
}
