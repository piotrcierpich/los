using System.ComponentModel.DataAnnotations;

namespace KoloLos.Models.GalleryManager
{
    public class GalleryDelete
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}