using System;

namespace KoloLos.Models
{
    public class ArticleAbstract
    {
        public int Id { get; set; }
        public string Abstract { get; set; }
        public DateTime PublishDate { get; set; }
        public string Title { get; set; }
    }
}