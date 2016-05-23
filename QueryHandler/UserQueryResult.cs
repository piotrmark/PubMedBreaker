

namespace QueryHandler
{
    public class UserQueryResult
    {
        public string ArticleTitle { get; set; }
        public string Abstract { get; private set; }
        public int Ranking { get; set; }
        public string ArticleId { get; set; }
    }
}
