using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using KoloLos.Models.Accounts;
using WebMatrix.WebData;

namespace KoloLos.Filters
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  public sealed class InitializeMembershipAttribute : ActionFilterAttribute
  {
    private static SimpleMembershipInitializer membershipInitializer;
    private static bool isInitialized;
    private static object initializationLock = new object();


    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      LazyInitializer.EnsureInitialized(ref membershipInitializer, ref isInitialized, ref initializationLock);
    }

    // ReSharper disable ClassNeverInstantiated.Local
    private class SimpleMembershipInitializer
    // ReSharper restore ClassNeverInstantiated.Local
    {
      public SimpleMembershipInitializer()
      {
        Database.SetInitializer<UsersContext>(null);

        try
        {
          WebSecurity.InitializeDatabaseConnection("KoloLos", "UserProfile", "UserId", "UserName", autoCreateTables: true);
          CreateDefaultUserIfNoneExists();
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
        }
      }

      private static void CreateDefaultUserIfNoneExists()
      {
        using (var context = new UsersContext())
        {
          if (context.UserProfiles.Any()) 
            return;

          WebSecurity.CreateUserAndAccount("admin", "admin");
          Roles.CreateRole("administrator");
          Roles.AddUserToRole("admin", "administrator");
        }
      }
    }
  }
}