using System.Web;
using System.Web.Routing;

namespace KoloLos.Models.Manager
{
    public class CategoryConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
                          RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.IncomingRequest)
            {
                if (values.ContainsKey("category"))
                {
                    string category = values["category"].ToString();
                    return ArticleCategories.IsValidCategory(category);
                }
            }
            return true;
        }
    }
}