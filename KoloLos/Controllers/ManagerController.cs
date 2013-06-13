using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KoloLosCommon;

namespace KoloLos.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ILosRepository losRepository;

        public ManagerController(ILosRepository losRepository)
        {
            this.losRepository = losRepository;
        }

        //
        // GET: /Manager/

        public ActionResult Index()
        {
            return View();
        }

    }
}
