using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KoloLos.Models.Manager
{
    public class ArticleNew
    {
        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Pole 'Tytuł' jest wymagane")]
        public string Title { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Pole 'Autor' jest wymagane")]
        public string Author { get; set; }

        [Display(Name = "Data publikacji")]
        [Required(ErrorMessage = "Pole 'Data publikacji' jest wymagane")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Pole 'Treść' jest wymagane")]
        [Display(Name = "Treść")]
        public string Content { get; set; }
    }
}