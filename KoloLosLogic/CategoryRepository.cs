using System;
using System.Data.Entity;
using System.Linq;

namespace KoloLosLogic
{
    public class CategoryRepository
    {
        private readonly IDbSet<Category> categories;

        public CategoryRepository(IDbSet<Category> categories)
        {
            this.categories = categories;
        }

        public Category GetByNameOrNull(string name)
        {
            return categories.FirstOrDefault(e => e.Name == name);
        }
    }
}
