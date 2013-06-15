using System;
using System.Web.Mvc;

namespace KoloLos.Models.Manager
{
    public class ArticleEdit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }

        [AllowHtml]
        public string Content { get; set; }
    }
}