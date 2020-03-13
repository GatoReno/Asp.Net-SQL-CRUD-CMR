using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashboardAdminFS_2.Controllers
{
    public class FinanzasController : Controller
    {
        // GET: Finanzas

        [Authorize(Roles = "finanzas,admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}