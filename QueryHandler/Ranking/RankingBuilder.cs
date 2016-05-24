using PubMedService;
using QueryHandler.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryHandler.Ranking
{
    public class RankingBuilder
    {
        private QueryTermsHandler termsHandler;
        private Document query; 
        private IList<PubMedArticleResult> matchedArticlesRanking;
        private IList<RankedTerm> queryTermsRankedWithIDF;
        
        public RankingBuilder(QueryTermsHandler termsHandler)
        {
            this.termsHandler = termsHandler;
        }

        public IList<PubMedArticleResult> Build(string queryStr, IList<PubMedQueryResult> matchedArticles,
            int resultsCount)
        {
            query = new Document(queryStr, termsHandler);
            matchedArticlesRanking = matchedArticles
                .Select(
                    art =>
                        new PubMedArticleResult(art, termsHandler))
                .ToList();

            queryTermsRankedWithIDF = ComputeInverseDocumentFrequencies();

            foreach (PubMedArticleResult article in matchedArticlesRanking)
                article.RankingVal = Math.Round(Match(query, article) + (resultsCount - article.PubMedPosition) * 0.01, 2);

            return matchedArticlesRanking;
        }

        private IList<RankedTerm> ComputeInverseDocumentFrequencies()
        {
            var result = new List<RankedTerm>();

            foreach (ComparableTerm term in query.Terms)
            {
                if (result.Any(t => t.Term.Equals(term)))
                    continue;

                var docsList = matchedArticlesRanking.Select(art => art.Document).ToList();
                docsList.Add(query);
                
                double idf = ComputeInverseDocumentFrequency(term, docsList);

                result.Add(new RankedTerm { Term = term, InverseDocumentFrequency = idf });
            }

            return result;
        }
        
        private double ComputeInverseDocumentFrequency(ComparableTerm term, IList<Document> documents)
        {
            double appearances = documents.Count(d => d.GetCountOf(term) > 0);
            double totalCount = documents.Count;
            return Math.Log(totalCount / appearances, 2);
        }

        private double Match(Document query, PubMedArticleResult article)
        {
            double scalarRatio = queryTermsRankedWithIDF
                .Select(
                    t =>
                        query.GetTFxIDF(t) * article.Document.GetTFxIDF(t))
                .Sum();

            double distanceRatio = Math.Sqrt(
                queryTermsRankedWithIDF
                .Select(
                    t =>
                        Math.Pow((query.GetTFxIDF(t) - article.Document.GetTFxIDF(t)), 2))
                .Sum());
            return scalarRatio / distanceRatio;
        }
    }
}
