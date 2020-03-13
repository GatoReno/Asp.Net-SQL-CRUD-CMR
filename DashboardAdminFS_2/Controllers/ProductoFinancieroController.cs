using DashboardAdminFS_2.Datos;
using DashboardAdminFS_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashboardAdminFS_2.Controllers
{
    public class ProductoFinancieroController : Controller
    {


        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public void InsertInBitacora(string id_user, string action, string coments)
        {

            using (DBEnt db = new DBEnt())
            {

                DateTime aDate = DateTime.Now;

                var bitacoraitem = new AdminBitacora()
                {
                    id_userCreated = id_user,
                    action = action,
                    comments = coments,
                    created_at = aDate,
                    id = RandomNumber(1, 1000000000)
                };

                db.AdminBitacora.Add(bitacoraitem);
                db.SaveChanges();
            }
        }

        // INSERTS
        [Authorize]
        [HttpPost]
        public ActionResult InsertInteres(string id_user, string interes, int id_producto, string name)
        {

            using (DBEnt db = new DBEnt())
            {

                DateTime aDate = DateTime.Now;
                var interestr = interes;
                interestr = interestr.Replace("%", "");

                var intereDeci = Convert.ToDecimal(interestr);
                var interx = new ProductoFinancieroInteres()
                {
                    id_productofinanciero = id_producto,
                    interes = intereDeci
                };

                db.ProductoFinancieroInteres.Add(interx);
                db.SaveChanges();
            }

            var action = "Insert";
            var comments = "Interes para Producto Financiero " + name + " ingresado por el usuario : ";
            InsertInBitacora(id_user, action, comments + id_user);



            return RedirectToAction("Details", "ProductoFinanciero", new { id = id_producto });

        }

        //begin updates

        [Authorize]
        [HttpPost]
        public ActionResult UpdateProductoFinanciero(EditProductoFin data)
        {

            var fx = data.files;
            if (fx == null)
            {
                using (DBEnt db = new DBEnt())
                {
                    var credito = db.HomeProductosFinancieros.ToList<HomeProductosFinancieros>().Where(u => u.id.Equals(data.id)).FirstOrDefault();




                    credito.CAT = data.CAT;
                    credito.descripcion = data.descripcion;
                    credito.edad = data.edad;
                    credito.esquemaDeCobro = data.esquemaDeCobro;
                    credito.frecuenciasDePago = data.frecuenciasDePago;
                    credito.grantiaPersonalAval = data.grantiaPersonalAval;
                    credito.lapMaxSemanas = data.lapMaxSemanas;
                    credito.lapMinSemanas = data.lapMinSemanas;
                    credito.montoCredito = data.montoCredito;
                    credito.numeroDeIntegrantes = data.numeroDeIntegrantes;
                    credito.otrasComisones = data.otrasComisones;
                    credito.plazoMaximo = data.plazoMaximo;
                    credito.plazoMinimo = data.plazoMinimo;
                    credito.requisitos = data.requisitos;
                    credito.soloclientes = data.soloclientes;
                    credito.tasaDeInteres = data.tasaDeInteres;
                    credito.tencologiaCrediticia = data.tencologiaCrediticia;
                    credito.tipocredito = data.tipocredito;
                    credito.tipoRetornoPago28Dias = data.tipoRetornoPago28Dias;
                    credito.tipoRetornoPagoCatorcenal = data.tipoRetornoPagoCatorcenal;
                    credito.tipoRetornoPagoSemanal = data.tipoRetornoPagoSemanal;
                    credito.title = data.title;

                    db.SaveChanges();
                    var action = "UPDATE";
                    var comments = "Producto Financiero actualizado por el usuario : ";

                    InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);

                    return RedirectToAction("Details", "ProductoFinanciero", new { id = data.id });
                }

            }
            else
            {

                using (DBEnt db = new DBEnt())
                {
                    var credito = db.HomeProductosFinancieros.ToList<HomeProductosFinancieros>().Where(u => u.id.Equals(data.id)).FirstOrDefault();
                    var filedelete = credito.img;
                    System.IO.File.Delete(filedelete);


                    DateTime dtime = new DateTime();
                    dtime = DateTime.Now;

                    int idint = RandomNumber(1, 1000000000);
                    string id = idint.ToString();
                    string filename = "" + "_" + id + "_" +
                                       String.Format("{0:d}", dtime) + data.files.FileName;
                    filename = filename.Replace("/", "_");


                    try
                    {
                        fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFiles\Creditos", filename));
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    var img = "/files/Creditos/" + filename;

                    credito.img = img;
                    credito.CAT = data.CAT;
                    credito.descripcion = data.descripcion;
                    credito.edad = data.edad;
                    credito.esquemaDeCobro = data.esquemaDeCobro;
                    credito.frecuenciasDePago = data.frecuenciasDePago;
                    credito.grantiaPersonalAval = data.grantiaPersonalAval;
                    credito.lapMaxSemanas = data.lapMaxSemanas;
                    credito.lapMinSemanas = data.lapMinSemanas;
                    credito.montoCredito = data.montoCredito;
                    credito.numeroDeIntegrantes = data.numeroDeIntegrantes;
                    credito.otrasComisones = data.otrasComisones;
                    credito.plazoMaximo = data.plazoMaximo;
                    credito.plazoMinimo = data.plazoMinimo;
                    credito.requisitos = data.requisitos;
                    credito.soloclientes = data.soloclientes;
                    credito.tasaDeInteres = data.tasaDeInteres;
                    credito.tencologiaCrediticia = data.tencologiaCrediticia;
                    credito.tipocredito = data.tipocredito;
                    credito.tipoRetornoPago28Dias = data.tipoRetornoPago28Dias;
                    credito.tipoRetornoPagoCatorcenal = data.tipoRetornoPagoCatorcenal;
                    credito.tipoRetornoPagoSemanal = data.tipoRetornoPagoSemanal;
                    credito.title = data.title;

                    var action = "UPDATE";
                    var comments = "Producto Financiero actualizado por el usuario : ";

                    InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);

                    db.SaveChanges();

                    return RedirectToAction("Details", "ProductoFinanciero", new { id = data.id });

                }
            }
        }


        //End Updates


        [Authorize]

        // GET: ProductoFinanciero/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [Authorize]
        public JsonResult JSONINFO(int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var credito = db.HomeProductosFinancieros.ToList<HomeProductosFinancieros>().Where(u => u.id.Equals(id)).FirstOrDefault();

                return Json(credito, JsonRequestBehavior.AllowGet);
            }
        }



        [Authorize]
        public JsonResult JSONInteres(int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var credito = db.ProductoFinancieroInteres.ToList<ProductoFinancieroInteres>().Where(u => u.id_productofinanciero.Equals(id));

                return Json(credito, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult JSONInfoUser(string id)
        {
            using (DBEnt db = new DBEnt())
            {
                var user = db.AspNetUsers.ToList<AspNetUsers>().Where(u => u.Id.Equals(id)).FirstOrDefault();

                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }





        /*
        // GET: ProductoFinanciero/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoFinanciero/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        */
        // GET: ProductoFinanciero/Delete/5

    }
}