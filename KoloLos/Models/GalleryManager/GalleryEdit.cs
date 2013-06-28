using System.ComponentModel.DataAnnotations;

namespace KoloLos.Models.GalleryManager
{
    public class GalleryEdit
    {
        public int Id { get; set; }

        [Display(Name = "Zdjęcie")]
        public string[] FileNames { get; set; }

        public string Title { get; set; }
    }
}