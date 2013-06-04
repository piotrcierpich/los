using System;

namespace KoloLosLogic
{
    public class Article
    {
        private DateTime publishDate = DateTime.Now;
        public virtual DateTime PublishDate
        {
            get { return publishDate; }
            set { publishDate = value; }
        }

        public virtual int Id { get; set; }
        public virtual string Content { get; set; }
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual string Abstract { get; set; }
        public virtual Category Category { get; set; }
    }
}