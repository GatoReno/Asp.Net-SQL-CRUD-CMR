using DashboardAdminFS_2.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashboardAdminFS_2.Controllers
{
    public class BeneficiosController : Controller
    {
        // GET: Beneficios
        [Authorize(Roles = "imagen,admin")]
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult JsonBeneficio(int id) {

            using (DBEnt db = new DBEnt())
            {
                var credito = db.HomeBeneficios.ToList<HomeBeneficios>().Where(u => u.id.Equals(id)).FirstOrDefault();

                return Json(credito, JsonRequestBehavior.AllowGet);
            }
        }
    }
}