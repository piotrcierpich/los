using System;

namespace KoloLosCommon
{
    public class Article
    {
        public readonly static Article Empty;

        static Article()
        {
            Empty = new Article
                        {
                                Id = 0,
                                Title = string.Empty,
                                Abstract = string.Empty,
                                Author = string.Empty,
                                Category = Category.Main,
                                Content = string.Empty,
                                PublishDate = DateTime.UtcNow
                        };
        }

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

        //TODO calculate it
        public virtual string Abstract { get; set; }
        public virtual Category Category { get; set; }
    }
}