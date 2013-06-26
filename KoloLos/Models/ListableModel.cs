using KoloLosCommon;

namespace KoloLos.Models
{
    public abstract class ListableModel
    {
        public Category Category { get; set; }
        public NextPreviousOptions NextPreviousOptions { get; set; }
    }
}