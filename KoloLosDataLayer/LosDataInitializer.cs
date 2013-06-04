using System;
using System.Data.Entity;

using KoloLosLogic;

namespace KoloLosDataLayer
{
    class LosDataInitializer : CreateDatabaseIfNotExists<LosDataContext>
    {
        protected override void Seed(LosDataContext context)
        {
            Category mainPageCategory = new Category
                                            {
                                                Name = "Main page"
                                            };

            context.Categories.Add(mainPageCategory);

            Article mainPageStory = new Article
                                   {
                                           Content = "Lorem ipsum dolor sit amet",
                                           Abstract = "Lorem ipsum dolor sit amet",
                                           Title = "Main page title",
                                           Author = "Me",
                                           Category = mainPageCategory,
                                           PublishDate = DateTime.Now
                                   };

            context.Articles.Add(mainPageStory);


            Category newsCategory = new Category
            {
                Name = "News"
            };

            context.Categories.Add(newsCategory);

            Article news1 = new Article
            {
                Content = "Lorem ipsum dolor sit amet",
                Abstract = "Lorem ipsum dolor sit amet",
                Title = "Title 2",
                Author = "Me",
                Category = newsCategory
            };

            context.Articles.Add(news1);

            Article news2 = new Article
            {
                Content = "Lorem ipsum dolor sit amet",
                Abstract = "Lorem ipsum dolor sit amet",
                Title = "Title 3",
                Author = "Me",
                Category = newsCategory
            };

            context.Articles.Add(news2);

            Article news3 = new Article
            {
                Content = "Lorem ipsum dolor sit amet",
                Abstract = "Lorem ipsum dolor sit amet",
                Title = "Title 4",
                Author = "Someone else",
                Category = newsCategory
            };

            context.Articles.Add(news3);

            context.SaveChanges();
        }
    }
}
