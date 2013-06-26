using System;
using System.Data.Entity;

using KoloLosCommon;

namespace KoloLosDataLayer
{
    class LosDataInitializer : CreateDatabaseIfNotExists<ArticlesDataContext>
    {
        private readonly DateTime baseDateTime = new DateTime(2012, 7, 11, 23, 25, 0);
        private const string LoremIpsumWithMarkup = "Lorem ipsum dolor sit amet, </br> consectetur adipisicing elit, <h1>sed do eiusmod tempor </h1>incididunt <p>ut labore </p>et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        private static readonly string LoremIpsumAbstract = LoremIpsumWithMarkup.Substring(0, 200) + "...";

        protected override void Seed(ArticlesDataContext context)
        {
            Article mainPageStory = new Article
                                   {
                                       Content = LoremIpsumWithMarkup,
                                       Title = "Main page title",
                                       Author = "Me",
                                       Category = Category.Main,
                                       PublishDate = DateTime.Now
                                   };

            context.Articles.Add(mainPageStory);

            Article history = new Article
                                  {
                                      Content = LoremIpsumWithMarkup,
                                      Abstract = LoremIpsumAbstract,
                                      Title = "Historia koła",
                                      Author = "Me",
                                      Category = Category.History,
                                      PublishDate = DateTime.Now
                                  };

            context.Articles.Add(history);

            Article contact = new Article
            {
                Content = LoremIpsumWithMarkup,
                Abstract = LoremIpsumAbstract,
                Title = "Kontakt",
                Author = "Me",
                Category = Category.Contact,
                PublishDate = DateTime.Now
            };

            context.Articles.Add(contact);

            for (int i = 0; i < 30; i++)
            {

                Article news1 = new Article
                {
                    Content = LoremIpsumWithMarkup,
                    Abstract = LoremIpsumAbstract,
                    Title = "Title " + i,
                    Author = "Me",
                    Category = Category.News,
                    PublishDate = baseDateTime.AddDays(i)
                };

                context.Articles.Add(news1);
            }

            for (int i = 0; i < 12; i++)
            {
                Article resolution = new Article
                {
                    Content = LoremIpsumWithMarkup,
                    Abstract = LoremIpsumAbstract,
                    Title = "Resolution title " + i,
                    Author = "Me",
                    Category = Category.Resolutions,
                    PublishDate = baseDateTime.AddDays(i)
                };

                context.Articles.Add(resolution);
            }

            for (int i = 0; i < 22; i++)
            {
                Gallery gallery = new Gallery
                                      {
                                              Path = "Sample1", 
                                              Title = "Sample gallery " + i,
                                              PublishDate = baseDateTime.AddDays(i)
                                      };
                context.Galleries.Add(gallery);
            }

            context.SaveChanges();
        }
    }
}
