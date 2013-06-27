using System;
using System.ComponentModel.DataAnnotations;

namespace KoloLos.Models.GalleryManager
{
    public class GalleryNew
    {


        [Required(ErrorMessage = "Pole tytuł jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Pole data publikacji jest wymagane")]
        [Display(Name = "Data publikacji")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
    }
}