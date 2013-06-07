namespace KoloLos.Models
{
    public class NewsList
    {
        public bool PreviousPageExists { get; set; }
        public bool NextPageExists { get; set; }
        public int NextPageIndex { get; set; }
        public int PreviousPageIndex { get; set; }

        public ArticleAbstract[] ArticleAbstracts { get; set; }
    }
}