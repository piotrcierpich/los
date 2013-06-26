namespace KoloLos.Models
{
    public class NextPreviousOptions
    {
        public const int EntriesPerPageMaxCount = 10;

        private readonly bool previousPageExists;
        private readonly bool nextPageExists;
        private readonly int nextPageIndex;
        private readonly int previousPageIndex;

        private NextPreviousOptions(bool previousPageExists, bool nextPageExists, int nextPageIndex, int previousPageIndex)
        {
            this.previousPageExists = previousPageExists;
            this.nextPageExists = nextPageExists;
            this.nextPageIndex = nextPageIndex;
            this.previousPageIndex = previousPageIndex;
        }

        public static NextPreviousOptions ForPageIndex(int pageIndex, int maxCount)
        {
            return new NextPreviousOptions
                    (
                    nextPageExists: AnyEntriesOnNextPage(pageIndex + 1, maxCount),
                    previousPageExists: pageIndex > 0,
                    previousPageIndex: pageIndex - 1,
                    nextPageIndex: pageIndex + 1
                    );
        }

        private static bool AnyEntriesOnNextPage(int nextPage, int maxEntriesCount)
        {
            return nextPage * EntriesPerPageMaxCount < maxEntriesCount;
        }



        public bool PreviousPageExists { get { return previousPageExists; } }
        public bool NextPageExists { get { return nextPageExists; } }
        public int NextPageIndex { get { return nextPageIndex; } }
        public int PreviousPageIndex { get { return previousPageIndex; } }
    }
}