using KoloLosCommon;

namespace KoloLos.Models
{
    public abstract class ListableModel
    {
        public Category Category { get; set; }
        public bool PreviousPageExists { get; set; }
        public bool NextPageExists { get; set; }
        public int NextPageIndex { get; set; }
        public int PreviousPageIndex { get; set; }
    }
}