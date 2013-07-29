namespace KoloLos.Models
{
    public class GalleryForSlideshow
    {
        public AvailableGallery[] AvailableGalleries { get; set; }
        public int? SelectedId { get; set; }
    }

    public class AvailableGallery
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public bool IsSelected { get; set; }
    }
}