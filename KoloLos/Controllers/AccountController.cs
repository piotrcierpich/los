using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KoloLos.Filters;
using KoloLos.Models.Accounts;
using WebMatrix.WebData;

namespace KoloLos.Controllers
{
    [Authorize]
    [InitializeMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LogIn logInModel, string returnUrl)
        {
          if (ModelState.IsValid)
          {
            if (WebSecurity.Login(logInModel.Login, logInModel.Password, logInModel.RememberMe))
              return RedirectToLocal(returnUrl);
          }
          ModelState.AddModelError("", "Login lub hasło są niepoprawne");
          return View(logInModel);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
          if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
          
          return RedirectToAction("Index", "Main");
        }
    }
}
