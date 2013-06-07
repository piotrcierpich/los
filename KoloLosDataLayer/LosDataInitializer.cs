using System;
using System.Data.Entity;

using KoloLosLogic;

namespace KoloLosDataLayer
{
    class LosDataInitializer : CreateDatabaseIfNotExists<LosDataContext>
    {
        private readonly DateTime baseDateTime = new DateTime(2012, 7, 11, 23, 25, 0);
        private const string LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        private const string AbstractLoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut...";

        protected override void Seed(LosDataContext context)
        {
            Category mainPageCategory = new Category
                                            {
                                                Name = "Main page"
                                            };

            context.Categories.Add(mainPageCategory);

            Article mainPageStory = new Article
                                   {
                                       Content = LoremIpsum,
                                       Abstract = AbstractLoremIpsum,
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

            for (int i = 0; i < 30; i++)
            {

                Article news1 = new Article
                {
                    Content = LoremIpsum,
                    Abstract = AbstractLoremIpsum,
                    Title = "Title " + i,
                    Author = "Me",
                    Category = newsCategory,
                    PublishDate = baseDateTime.AddDays(i)
                };

                context.Articles.Add(news1);
            }

            context.SaveChanges();
        }
    }
}
