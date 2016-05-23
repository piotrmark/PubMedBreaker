using PubMedService;
using QueryHandler.Engine;
using QueryHandler.Ranking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeSHService.Helpers.StringExtensions;

namespace QueryHandler
{
    public class PubMedArticleResult
    {
        public double RankingVal { get; set; }

        public string Title { get; private set; }
        public string Abstract { get; private set; }
        public string PubMedId { get; private set; }
        public Document Document { get; private set;}
        public int PubMedPosition { get; private set; }

        public PubMedArticleResult(PubMedQueryResult matchedArticle, QueryTermsHandler termsHandler)
        {
            Title = matchedArticle.Article.Title;
            Abstract = matchedArticle.Article.Abstract;
            PubMedId = matchedArticle.Article.PubMedId;
            PubMedPosition = matchedArticle.PubMedIndex;
            Document = new Document(matchedArticle.Article.Title + matchedArticle.Article.Abstract, termsHandler);
        }
    }
}
