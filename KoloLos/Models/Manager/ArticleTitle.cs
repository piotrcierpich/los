using System.ComponentModel.DataAnnotations;

namespace KoloLos.Models.Manager
{
    public class ArticleTitle
    {
        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }
    }
}