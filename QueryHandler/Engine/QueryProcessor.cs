using MeSHService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeSHService.Helpers.StringExtensions;

namespace QueryHandler.Engine
{
    public class QueryProcessor
    {
        MeshService service;

        StringTransformation unifyCase = (string source) => { return source.ToLower(); };

        public QueryProcessor()
        {
            service = new MeshService(unifyCase);
        }

        public List<string> GetQueriesToSend(string userQuery)
        {
            return new List<string> { GetSynonymBasedQuery(userQuery) };
        }

        private string GetSynonymBasedQuery(string userQuery)
        {
            //TODO: Construct query from list of phrases with matching synonyms
            return string.Empty;
        }

        private List<Tuple<string, IList<string>>> ReplaceWithSynonyms(string userQuery)
        {
            var result = new List<Tuple<string, IList<string>>>();

            var words = userQuery.Split().ToList();
            for (int termLength = words.Count; termLength > 1; termLength--)
            {
                for (int i = 0; i + termLength <= words.Count; i++)
                {
                    string currentlyChecked = unifyCase(words.JoinRange(i, termLength));
                    IList<string> synonyms = service.GetExactSynonyms(currentlyChecked);
                    if(synonyms.Any())
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

            foreach(string word in words)
                result.Add(new Tuple<string, IList<string>>(word, service.GetSynonyms(word)));

            return result;
        }
    }
}
