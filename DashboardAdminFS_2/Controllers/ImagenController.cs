using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashboardAdminFS_2.Controllers
{
    public class ImagenController : Controller
    {
        // GET: Imagen
        [Authorize(Roles = "imagen,admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}