using System;
using System.Linq;

using KoloLosCommon;

namespace KoloLos.Models.Manager
{
    public class ArticleCategories
    {
        public static bool IsValidCategory(string category)
        {
            return Enum.GetNames(typeof(Category)).Any(validCat => StringComparer.OrdinalIgnoreCase.Equals(validCat, category));
        }
    }
}