using DashboardAdminFS_2.Datos;
using DashboardAdminFS_2.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashboardAdminFS_2.Controllers
{
    public class DashboardAdminController : Controller
    {

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // GET: DashboardAdmin

        private readonly UserManager<ApplicationUser> _userManager;
        [Authorize]

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "imagen,admin")]
        public ActionResult Promociones()
        {


            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminRoles() {

            return View();
        }

        public ActionResult InsertBeneficios()
        {

            return View();
        }

        public ActionResult InsertValores() {
            return View();
        }

        public ActionResult InsertCodigoEtica() {
            return View();
        }
        [Authorize(Roles = "imagen,admin")]
        public ActionResult Mapa()
        {
            return View();
        }
        [Authorize(Roles = "imagen,admin")]
        public ActionResult Videos()
        {
            return View();
        }
        [Authorize(Roles = "imagen,admin")]
        public ActionResult Carrousel()
        {

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Bitacora()
        {

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Calculadora()
        {

            return View();
        }
        

        [Authorize(Roles = "direc.Administracion,admin")]
        public ActionResult DireccionAdministracion()
        {

            return View();
        }

        [Authorize(Roles = "geren.Operaciones,admin")]
        public ActionResult GerenciaOperacional()
        {

            return View();
        }

        //geren.Operaciones
        [Authorize(Roles = "finanzas,admin")]
        public ActionResult Anuales()
        {

            return View();
        }


        [Authorize(Roles = "finanzas,admin")]
        public ActionResult Trimestrales()
        {

            return View();
        }


        [Authorize(Roles = "finanzas,admin")]
        public ActionResult Mensuales()
        {

            return View();
        }

        public ActionResult EditBeneficios(int Id) {
            ViewBag.Id = Id;
            return View();
        }

        public ActionResult EditValores()
        {
            // ViewBag.Id = Id;
            return View();
        }
        public ActionResult EditCodigoDeEtica() {

            //ViewBag.Id = Id;
            return View();
        }

        [HttpGet]
        [Route("PermisionsRoles/{n}")]
        public ActionResult PermisionsRoles(string n)
        {
            using (DBEnt db = new DBEnt())
            {
                var user = db.AspNetUsers.ToList<AspNetUsers>().Where(u => u.Id.Equals(n)).FirstOrDefault();
                

                if (user != null)
                {
                    ViewBag.Id = n;
                    return View();
                }
                else {
                    return RedirectToAction("AdminRoles");
                }
            }
        }

        // INSERTS
        // AQUI
        [Authorize]
        [HttpPost]
        public ActionResult InsertDatosContacto(string id_userCreated, string direccion, string telefono, string email)
        {

            using (DBEnt db = new DBEnt())
            {

                var contacto = new HomeContactoDatos()
                {
                    direccion = direccion,
                    telefono = telefono,
                    email = email

                };
                db.HomeContactoDatos.Add(contacto);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Datos de contactos ingresada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult InsertCodigoDeEtica(string id_userCreated, string texto, string textoHead)
        {

            using (DBEnt db = new DBEnt())
            {

                var codigo = new HomeCodigoDeEtica()
                {
                    texto = texto,
                    textoHead = textoHead

                };
                db.HomeCodigoDeEtica.Add(codigo);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Codigo De Etica ingresado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult InsertValores(string id_userCreated, string texto, string textoHead)
        {

            using (DBEnt db = new DBEnt())
            {

                var valores = new HomeValores()
                {
                    texto = texto,
                    textoHead = textoHead

                };
                db.HomeValores.Add(valores);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Valores ingresados por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult InsertBeneficios(string id_userCreated, string texto, string icono)
        {

            using (DBEnt db = new DBEnt())
            {

                var contacto = new HomeBeneficios()
                {
                    texto = texto,
                    icono = icono

                };
                db.HomeBeneficios.Add(contacto);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Beneficios ingresado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }


        [Authorize]
        [HttpPost]
        public ActionResult InsertAnuncioCookies(string id_userCreated, HttpPostedFileBase datafiles, string descripcion)
        {


            var fx = datafiles;
            DateTime dtime = new DateTime();
            dtime = DateTime.Now;

            int idint = RandomNumber(1, 1000000000);
            string id = idint.ToString();
            string filename = "AnuncioCookies" + "_" + id + "_" +
                               String.Format("{0:d}", dtime) + datafiles.FileName;
            filename = filename.Replace("/", "_");

            try
            {

                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\Imgs", filename));
                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\Imgs", filename));
            }
            catch (Exception)
            {

                throw;
            }
            var path = "/WebFiles/Imgs/" + filename;


            using (DBEnt db = new DBEnt())
            {

                var contacto = new HomeAnuncioCookies()
                {
                    path = path,
                    descripcion = descripcion


                };
                db.HomeAnuncioCookies.Add(contacto);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Anuncio de cookies ingresada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        public ActionResult InsertMission(MisionVisionForm data)
        {

            var fx = data.files;
            DateTime dtime = new DateTime();
            dtime = DateTime.Now;

            int idint = RandomNumber(1, 1000000000);
            string id = idint.ToString();
            string filename = "Mision" + "_" + id + "_" +
                               String.Format("{0:d}", dtime) + data.files.FileName;
            filename = filename.Replace("/", "_");

            try
            {

                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\Imgs", filename));
                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\Imgs", filename));
            }
            catch (Exception)
            {

                throw;
            }
            var path = "/WebFiles/Imgs/" + filename;

            using (DBEnt db = new DBEnt())
            {


                var mission = new HomeMision()
                {
                    text = data.text,
                    img = path

                };
                db.HomeMision.Add(mission);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "mision ingresada por el usuario : ";
            InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);

            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        public ActionResult InsertVission(MisionVisionForm data)
        {

            var fx = data.files;
            DateTime dtime = new DateTime();
            dtime = DateTime.Now;

            int idint = RandomNumber(1, 1000000000);
            string id = idint.ToString();
            string filename = "Vision" + "_" + id + "_" +
                               String.Format("{0:d}", dtime) + data.files.FileName;
            filename = filename.Replace("/", "_");

            try
            {

                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\Imgs", filename));
                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\Imgs", filename));
            }
            catch (Exception)
            {

                throw;
            }
            var path = "/WebFiles/Imgs/" + filename;

            using (DBEnt db = new DBEnt())
            {


                var vision = new HomeVision()
                {
                    text = data.text,
                    img = path

                };
                db.HomeVision.Add(vision);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "vision ingresada por el usuario : ";
            InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);

            return RedirectToAction("index", "imagen");
        }
        [Authorize]
        [HttpPost]
        public ActionResult InsertExtraData(MisionVisionForm data)
        {

            var fx = data.files;
            DateTime dtime = new DateTime();
            dtime = DateTime.Now;

            int idint = RandomNumber(1, 1000000000);
            string id = idint.ToString();
            string filename = "Extra" + "_" + id + "_" +
                               String.Format("{0:d}", dtime) + data.files.FileName;
            filename = filename.Replace("/", "_");

            try
            {

                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\Imgs", filename));
                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\Imgs", filename));
            }
            catch (Exception)
            {

                throw;
            }
            var path = "/WebFiles/Imgs/" + filename;

            using (DBEnt db = new DBEnt())
            {


                var extra = new HomeDatosExtra()
                {
                    text = data.text,
                    img = path

                };
                db.HomeDatosExtra.Add(extra);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "datos extra ingresada por el usuario : ";
            InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);

            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        public  ActionResult InsertBarraItem(BarrasForm data)
        {
            
            using (DBEnt db = new DBEnt())
            {


                var barra = new HomeBarras()
                {
                    text = data.text,
                    color = data.color,
                    valor = data.valor

                };
                db.HomeBarras.Add(barra); //insert into homebarras (obj) values (obj)
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "barra ingresada por el usuario : ";
            InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);

            return RedirectToAction("index", "imagen");
        }
        [Authorize]
        [HttpPost]
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

        [Authorize]
        [HttpPost]
        public ActionResult InsertVideoItem(HomeVideoITems formvideodata)
        {

            using (DBEnt db = new DBEnt())
            {

                var vitem = new HomeVideoITems()
                {
                    datafilter = formvideodata.datafilter,
                    id_userCreated = formvideodata.id_userCreated,
                    urlyt = formvideodata.urlyt,
                    imngurlyt = formvideodata.imngurlyt

                };
                db.HomeVideoITems.Add(vitem);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "video ingresado por el usuario : ";
            InsertInBitacora(formvideodata.id_userCreated, action, comments + formvideodata.id_userCreated);

            return RedirectToAction("index", "imagen");
        }


        [Authorize]
        [HttpPost]
        public ActionResult InsertVideoCategoria(HomeVideoCategorias formvideodata)
        {

            using (DBEnt db = new DBEnt())
            {

                var cat = new HomeVideoCategorias()
                {
                    filterCategoria = formvideodata.filterCategoria,
                    id_userCreated = formvideodata.id_userCreated
                };
                db.HomeVideoCategorias.Add(cat);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "categoría ingresada por el usuario : " + formvideodata.id_userCreated;
            InsertInBitacora(formvideodata.id_userCreated, action, comments + formvideodata.id_userCreated);
            return RedirectToAction("index", "imagen");
        }



        [Authorize]
        [HttpPost]
        public ActionResult InsertSucursal(string id_userCreated, string lat,string longi, string sucursal,string telefono, string direc)
        {

            using (DBEnt db = new DBEnt())
            {

                var suc = new HomeSucursales()
                {
                    longitud = longi,
                    latitud = lat,
                    sucursal = sucursal,
                    telefono = telefono,
                    direccion = direc
                };
                db.HomeSucursales.Add(suc);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Sucursal ingresada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }


        [Authorize]
        [HttpPost]
        public ActionResult InsertCounters(string id_userCreated, string texto, string value)
        {

            using (DBEnt db = new DBEnt())
            {

                var counter = new HomeCounters()
                {
                    text = texto,
                    value = value
                    // id = RandomNumber(1, 1000000000)

                };
                db.HomeCounters.Add(counter);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Contadores ingresada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        public ActionResult InsertPromocion(Promociones data)
        {
            var fx = data.files;
            DateTime dtime = new DateTime();
            dtime = DateTime.Now;

            int idint = RandomNumber(1, 1000000000);
            string id = idint.ToString();
            string filename = "Promociones" + "_" + id + "_" +
                               String.Format("{0:d}", dtime) + data.files.FileName;
            filename = filename.Replace("/", "_");

            try
            {

                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\Promociones", filename));
                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\Promociones", filename));
            }
            catch (Exception)
            {

                throw;
            }
            var path = "/WebFiles/Promociones/" + filename;


            using (DBEnt db = new DBEnt())
            {

                var prom = new HomePromociones()
                {
                    title = data.title,
                    path = path

                };
                db.HomePromociones.Add(prom);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Promocion ingresada por el usuario : ";
            InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);
            return RedirectToAction("index", "imagen");
        }
       
        /*Finanzas*/
        [Authorize]
        [HttpPost]
        public ActionResult InsertProductoFinanciero(ProductoFinanciero data)
        {


            var fx = data.files;


            DateTime dtime = new DateTime();
            dtime = DateTime.Now;

            int idint = RandomNumber(1, 1000000000);
            string id = idint.ToString();
            string filename = "" + "_" + id + "_" +
                               String.Format("{0:d}", dtime) + data.files.FileName;
            filename = filename.Replace("/", "_");
            var maxSemanas = data.plazoMaximo / 7;
            var minSemanas = data.plazoMinimo / 7;

            try
            {
                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\Creditos", filename));
                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\Creditos", filename));
            }
            catch (Exception)
            {

                throw;
            }

            var img = "/WebFiles/Creditos/" + filename;

            var ddt = String.Format("{0:d}", dtime);



            using (DBEnt db = new DBEnt())
            {

                var credit = new HomeProductosFinancieros()
                {
                    img = img,
                    title = data.title,
                    descripcion = data.descripcion,
                    montoCredito = data.montoCredito,
                    plazoMaximo = data.plazoMaximo,
                    plazoMinimo = data.plazoMinimo,
                    frecuenciasDePago = data.frecuenciasDePago,
                    esquemaDeCobro = data.esquemaDeCobro,
                    requisitos = data.requisitos,
                    tasaDeInteres = data.tasaDeInteres,
                    CAT = data.CAT,
                    created_at = dtime,
                    lastupdate = dtime,
                    id_userCreated = data.id_userCreated,
                    lapMaxSemanas = maxSemanas,
                    lapMinSemanas = minSemanas,
                    edad = data.edad,
                    numeroDeIntegrantes = data.numeroDeIntegrantes,
                    otrasComisones = data.otrasComisones,
                    tipocredito = data.tipocredito,
                    grantiaPersonalAval = data.grantiaPersonalAval,
                    tencologiaCrediticia = data.tencologiaCrediticia,
                    tipoRetornoPago28Dias = data.tipoRetornoPago28Dias,
                    tipoRetornoPagoCatorcenal = data.tipoRetornoPagoCatorcenal,
                    tipoRetornoPagoSemanal = data.tipoRetornoPagoSemanal,
                    costoTotalAnualCredito = data.costoTotalAnualCredito


                };
                db.HomeProductosFinancieros.Add(credit);
                //db..add(cat);
                db.SaveChanges();

                var interestr = data.interes;
                interestr = interestr.Replace("%", "");

                var intereDeci = Convert.ToDecimal(interestr);

                var interes = new ProductoFinancieroInteres()
                {

                    id_productofinanciero = credit.id,
                    interes = intereDeci
                };

                db.ProductoFinancieroInteres.Add(interes);
                db.SaveChanges();



            }

            var action = "Insert";
            var comments = "Producto Financiero ingresado por el usuario : ";
            InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);

            return RedirectToAction("index", "finanzas");

        }

        [Authorize]
        [HttpPost]
        public ActionResult InsertUNE(ProductoFinanciero data)
        {


            var fx = data.files;
            DateTime aDate = DateTime.Now;

            int idint = RandomNumber(1, 1000000000);
            string id = idint.ToString();
            string filename = "" + "_" + id + "_" +
                               String.Format("{0:d}", aDate) + data.files.FileName;
            filename = filename.Replace("/", "_");

            try
            {

                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\UNE", filename));
                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\UNE", filename));
            }
            catch (Exception)
            {

                throw;
            }

            var path = "/WebFiles/UNE/" + filename;

            using (DBEnt db = new DBEnt())
            {

                var une = new HomeUNE()
                {

                    created_at = aDate,
                    lastupdate = aDate,
                    path = path,
                    id = idint,
                    id_userCreated = data.id_userCreated


                };
                db.HomeUNE.Add(une);
                //db..add(cat);
                db.SaveChanges();
            }

            var action = "Insert";
            var comments = "Producto Financiero ingresado por el usuario : ";
            InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);

            return RedirectToAction("index", "finanzas");

        }


        [Authorize]
        [HttpPost]
        public ActionResult InsertCostoComisiones(ProductoFinanciero data)
        {


            var fx = data.files;


            DateTime dtime = new DateTime();
            dtime = DateTime.Now;

            int idint = RandomNumber(1, 1000000000);
            string id = idint.ToString();
            string filename = "" + "_" + id + "_" +
                               String.Format("{0:d}", dtime) + data.files.FileName;
            filename = filename.Replace("/", "_");

            try
            {
                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\Costosycomisiones", filename));
                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\Costosycomisiones", filename));
            }
            catch (Exception)
            {

                throw;
            }

            var img = "/WebFiles/Costosycomisiones/" + filename;

            var ddt = String.Format("{0:d}", dtime);



            using (DBEnt db = new DBEnt())
            {

                var costos = new HomeCostosComisiones()
                {

                    id_userCreated = data.id_userCreated,
                    path = img,
                    created_at = dtime,
                    id = RandomNumber(1, 1000000000)


                };
                db.HomeCostosComisiones.Add(costos);
                //db..add(cat);
                db.SaveChanges();
            }

            var action = "Insert";
            var comments = "Documento Costos y Comisiones ingresado por el usuario : ";
            InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);

            return RedirectToAction("index", "finanzas");

        }  

        [Authorize]
        [HttpPost]
        public ActionResult InsertInformacionFinanciera(InfoFin data)
        {


            var fx = data.files;
            var contentT = data.files.ContentType;


            DateTime dtime = new DateTime();
            dtime = DateTime.Now;

            int idint = RandomNumber(1, 1000000000);
            string id = idint.ToString();
            string filename = "" + "_" + id + "_" +
                               String.Format("{0:d}", dtime) + data.files.FileName;
            filename = filename.Replace("/", "_");


            var yr = data.datecorresponde.Year;
            var mth = data.datecorresponde.Month;
            var dy = data.datecorresponde.Day;
            var path = "";
            switch (data.tipo)
            {
                case "mensual":


                    fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\InfoFin\mensual", filename));
                    fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\InfoFin\mensual", filename));


                    path = "/WebFiles/InfoFin/mensual/" + filename;
                    break;
                case "trimestral":

                    fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\InfoFin\trimestral", filename));
                    fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\InfoFin\trimestral", filename));


                    path = "/WebFiles/InfoFin/trimestral/" + filename;
                    break;
                case "anual":
                    fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\InfoFin\anual", filename));
                    fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\InfoFin\anual", filename));



                    path = "/WebFiles/InfoFin/anual/" + filename;
                    break;
            }




            var ddt = String.Format("{0:d}", dtime);



            using (DBEnt db = new DBEnt())
            {

                var info = new HomeInfoFinanciera()
                {
                    id = RandomNumber(1, 1000000000),
                    path = path,
                    tipo = data.tipo,
                    id_userCreated = data.id_userCreated,
                    created_at = dtime,
                    lastupdate = dtime,
                    year = yr,
                    month = mth,
                    day = dy
                };
                db.HomeInfoFinanciera.Add(info);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Información financiera " + data.tipo + " ingresado por el usuario : ";
            InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);
            return RedirectToAction("index", "finanzas");

        }


        // DELETE FINANZAS
        [Authorize]
        [HttpPost]
        public ActionResult DeleteUNE(string id_userCreated, string id)
        {

            using (DBEnt db = new DBEnt())
            {
                var x = id_userCreated;


                var dbx = db.HomeUNE.Where(u => u.id.Equals(id)).FirstOrDefault();


                var myPath = dbx.path;
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + myPath);
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + myPath);
                db.HomeUNE.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "UNE eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "finanzas");

        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteMensualInfoFin(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var x = id_userCreated;


                var dbx = db.HomeInfoFinanciera.Where(u => u.id.Equals(id)).FirstOrDefault();

                var myPath = dbx.path;
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + myPath);
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + myPath);

                db.HomeInfoFinanciera.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Información financiera mensual eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "finanzas");

        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteAnualInfoFin(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var x = id_userCreated;


                var dbx = db.HomeInfoFinanciera.Where(u => u.id.Equals(id)).FirstOrDefault();

                var myPath = dbx.path;
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + myPath);
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + myPath);





                db.HomeInfoFinanciera.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Información financiera anual eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "finanzas");

        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteTrimestralInfoFin(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var x = id_userCreated;


                var dbx = db.HomeInfoFinanciera.Where(u => u.id.Equals(id)).FirstOrDefault();


                var myPath = dbx.path;
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + myPath);
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + myPath);

                db.HomeInfoFinanciera.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Información financiera trimestral eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "finanzas");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteInteres(int id, string id_userCreated)
        {

            using (DBEnt db = new DBEnt())
            {



                var dbx = db.ProductoFinancieroInteres.Where(u => u.id.Equals(id)).FirstOrDefault();



                db.ProductoFinancieroInteres.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Interes  eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "finanzas");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteProductosFinanciero(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var x = id_userCreated;


                var dbx = db.HomeProductosFinancieros.Where(u => u.id.Equals(id)).FirstOrDefault();
                var myPath = dbx.img;
               // System.IO.File.Delete(myPath);
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + myPath);
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + myPath);
                db.HomeProductosFinancieros.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Productos Financiero eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "finanzas");
        }


        /*FINAL FINANZAS*/
        [Authorize]
        [HttpPost]
        public ActionResult InsertItemCarrousel(CarrouselItem data)
        {


            var fx = data.files;
            var contentT = data.files.ContentType;


            DateTime dtime = new DateTime();
            dtime = DateTime.Now;

            int idint = RandomNumber(1, 1000000000);
            string id = idint.ToString();
            string filename = "" + "_" + id + "_" +
                               String.Format("{0:d}", dtime) + data.files.FileName;
            filename = filename.Replace("/", "_");

            try
            {


                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\Carrousel", filename));
                fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\Carrousel", filename));

            }
            catch (Exception)
            {

                throw;
            }

            var img = "/WebFiles/carrousel/" + filename;

            var ddt = String.Format("{0:d}", dtime);



            using (DBEnt db = new DBEnt())
            {

                var car = new HomeCarrouselItems()
                {
                    img = img,
                    h2tag = data.h2tag,
                    ptag = data.ptag,
                    id_userCreated = data.id_userCreated


                };
                db.HomeCarrouselItems.Add(car);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Carrousel Item ingresado por el usuario : ";
            InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);
            return RedirectToAction("index", "imagen");
        }


     

        [Authorize]
        [HttpPost]
        public ActionResult InsertValoresDeLaEmpresa() {

            return View("Index");
        }


        [Authorize]
        [HttpPost]
        public ActionResult InsertRole(string role, string id_userCreated)
        {

            using (DBEnt db = new DBEnt())
            {

                var rolx = new AspNetRoles()
                {
                    Name = role,
                    Id = RandomString(11)

                };
                db.AspNetRoles.Add(rolx);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Contadores ingresada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("AdminRoles");
        }

        [Authorize]
        [HttpPost]
        public ActionResult SetRoleUser(string role, string id_user, string id_userCreated)
        {

            using (DBEnt db = new DBEnt())
            {

                var roleuser = new AspNetUserRoles()
                {
                    RoleId = role,
                    UserId = id_user

                };
                db.AspNetUserRoles.Add(roleuser);
                //db..add(cat);
                db.SaveChanges();
            }
            var action = "Insert";
            var comments = "Role asiganado a usario :" + id_user + " por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("AdminRoles");
        }

        // END INSERTS



        // DELETES

        [Authorize]
        [HttpPost]
        public ActionResult DeleteVideoCategoria(string id_userCreated, int id, string filterCategoria)
        {

            using (DBEnt db = new DBEnt())
            {
                var x = id_userCreated;


                var dbx = db.HomeVideoCategorias.Where(u => u.id.Equals(id)).FirstOrDefault();
                db.HomeVideoCategorias.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "video eliminado por el usuario : ";

            InsertInBitacora(id_userCreated, action, comments + id_userCreated);


            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCarrousellItem(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var x = id_userCreated;


                var dbx = db.HomeCarrouselItems.Where(u => u.id.Equals(id)).FirstOrDefault();

                var myPath = dbx.img;

                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + myPath);
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + myPath);
                db.HomeCarrouselItems.Remove(dbx);

                db.SaveChanges();
            }


            var action = "Delete";
            var comments = "Carrousel Item eliminado por el usuario : ";

            InsertInBitacora(id_userCreated, action, comments + id_userCreated);


            return RedirectToAction("index", "imagen");
        }

     

       

        [Authorize]
        [HttpPost]
        public ActionResult DeleteSucursal(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var x = id_userCreated;


                var dbx = db.HomeSucursales.Where(u => u.id.Equals(id)).FirstOrDefault();
                db.HomeSucursales.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Sucursal eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteBeneficios(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var x = id_userCreated;


                var dbx = db.HomeBeneficios.Where(u => u.id.Equals(id)).FirstOrDefault();
                db.HomeBeneficios.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Sucursal eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteValores(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var x = id_userCreated;


                var dbx = db.HomeValores.Where(u => u.id.Equals(id)).FirstOrDefault();
                db.HomeValores.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Valores eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCodigoEtica(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var x = id_userCreated;


                var dbx = db.HomeCodigoDeEtica.Where(u => u.id.Equals(id)).FirstOrDefault();
                db.HomeCodigoDeEtica.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = " Codigo De Etica eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }
      

        [Authorize]
        [HttpPost]
        public ActionResult DeleteVideoYT(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {


                var dbx = db.HomeVideoITems.Where(u => u.id.Equals(id)).FirstOrDefault();

                db.HomeVideoITems.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Información financiera trimestral eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }


        [Authorize]
        [HttpPost]
        public ActionResult DeleteCostosYComisiones(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {


                var dbx = db.HomeCostosComisiones.Where(u => u.id.Equals(id)).FirstOrDefault();


                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + dbx.path);
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + dbx.path);

                db.HomeCostosComisiones.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Documento de Costos y Comisiones eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return View("Index");
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeletePromocion(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {


                var dbx = db.HomePromociones.Where(u => u.id.Equals(id)).FirstOrDefault();



                var myPath = dbx.path;
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + myPath);
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + myPath);


                db.HomePromociones.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Promocion eliminada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteVision(string id_userCreated)
        {

            using (DBEnt db = new DBEnt())
            {


                var q = db.HomeVision.ToList<HomeVision>();


                foreach (var c in q)

                {
                    db.HomeVision.Remove(c); //Note: db is your EntityObject

                    System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + c.img);
                    System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + c.img);
                }

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Vision eliminada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteMision(string id_userCreated)
        {

            using (DBEnt db = new DBEnt())
            {


                var q = db.HomeMision.ToList<HomeMision>();


                foreach (var c in q)

                {
                    db.HomeMision.Remove(c); //Note: db is your EntityObject

                    System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + c.img);
                    System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + c.img);
                }


                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Mision eliminada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteDatosExtra(string id_userCreated)
        {

            using (DBEnt db = new DBEnt())
            {


                var q = db.HomeDatosExtra.ToList<HomeDatosExtra>();


                foreach (var c in q)

                {
                    db.HomeDatosExtra.Remove(c); //Note: db is your EntityObject

                    System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + c.img);
                    System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + c.img);
                }


                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Datos Extra eliminada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteBarra(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var dbx = db.HomeBarras.Where(u => u.id.Equals(id)).FirstOrDefault();
                db.HomeBarras.Remove(dbx); //Note: db is your EntityObject
                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Barra eliminada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }
        //HomeCounters
        [Authorize]
        [HttpPost]
        public ActionResult DeleteCounter(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var dbx = db.HomeCounters.Where(u => u.id.Equals(id)).FirstOrDefault();
                db.HomeCounters.Remove(dbx); //Note: db is your EntityObject
                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Counter eliminada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteContactoDatos(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var dbx = db.HomeContactoDatos.Where(u => u.id.Equals(id)).FirstOrDefault();
                db.HomeContactoDatos.Remove(dbx); //Note: db is your EntityObject
                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "ContactoDatos eliminados por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCookiesAviso(string id_userCreated, int id)
        {

            using (DBEnt db = new DBEnt())
            {


                var dbx = db.HomeAnuncioCookies.Where(u => u.id.Equals(id)).FirstOrDefault();



                var myPath = dbx.path;
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + myPath);
                System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + myPath);


                db.HomeAnuncioCookies.Remove(dbx);

                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Aviso de Cookies eliminada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("index", "imagen");
        }
        [Authorize]
        [HttpPost]

        //ADMIN 
        public ActionResult DeleteRole(string id_userCreated, string id) {

            using (DBEnt db = new DBEnt()) {
                var dbx = db.AspNetRoles.Where(u => u.Id.Equals(id)).FirstOrDefault();

                db.AspNetRoles.Remove(dbx);
                db.SaveChanges();
            }
            var action = "Delete";
            var comments = "Role eliminado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);

            return RedirectToAction("AdminRoles");
        }

        // END DELETES







        //GET JSON LIST

        [Authorize]
        [HttpGet]
        public JsonResult VideosCategoriasListJson()
        {

            using (DBEnt db = new DBEnt())
            {

                var categorias = db.HomeVideoCategorias.ToList<HomeVideoCategorias>();

                return Json(categorias, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [HttpGet]
        public JsonResult BitacoraListJson()
        {

            using (DBEnt db = new DBEnt())
            {

                var categorias = db.AdminBitacora.ToList<AdminBitacora>();

                return Json(categorias, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [HttpGet]
        public JsonResult CarrouselItemsListJson()
        {

            using (DBEnt db = new DBEnt())
            {

                var carrItems = db.HomeCarrouselItems.ToList<HomeCarrouselItems>();

                return Json(carrItems, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [HttpGet]
        public JsonResult VideosListJson()
        {

            using (DBEnt db = new DBEnt())
            {

                var categorias = db.HomeVideoITems.ToList<HomeVideoITems>();

                return Json(categorias, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [HttpGet]
        public JsonResult ProductosFinancierosListJson()
        {


            using (DBEnt db = new DBEnt())
            {
                var credits = db.HomeProductosFinancieros.ToList<HomeProductosFinancieros>();

                return Json(credits, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult JsonProductosFinanciero()
        {


            using (DBEnt db = new DBEnt())
            {
                var credits = db.HomeProductosFinancieros.ToList<HomeProductosFinancieros>();

                return Json(credits, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsonPF()
        {
            using (DBEnt db = new DBEnt())
            {
                var CostoComisiones = db.HomeProductosFinancieros.ToList<HomeProductosFinancieros>();

                return Json(CostoComisiones, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult UNEListJson()
        {
            using (DBEnt db = new DBEnt())
            {
                var UNES = db.HomeUNE.ToList<HomeUNE>();

                return Json(UNES, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult CostoComisionesListJson()
        {
            using (DBEnt db = new DBEnt())
            {
                var CostoComisiones = db.HomeCostosComisiones.ToList<HomeCostosComisiones>();

                return Json(CostoComisiones, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult InfoFinAnualListJson()
        {
            using (DBEnt db = new DBEnt())
            {
                var info = db.HomeInfoFinanciera.Where(u => u.tipo.Equals("anual")).ToList<HomeInfoFinanciera>();

                return Json(info, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult InfoFinMensualListJson()
        {
            using (DBEnt db = new DBEnt())
            {
                var info = db.HomeInfoFinanciera.Where(u => u.tipo.Equals("mensual")).ToList<HomeInfoFinanciera>();

                return Json(info, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult InfoFinTrimestralListJson()
        {
            using (DBEnt db = new DBEnt())
            {
                var info = db.HomeInfoFinanciera.Where(u => u.tipo.Equals("trimestral")).ToList<HomeInfoFinanciera>();

                return Json(info, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult sucursalesListJson()
        {
            using (DBEnt db = new DBEnt())
            {
                var user = db.HomeSucursales.ToList<HomeSucursales>();

                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize]
        //[HttpGet]
        public JsonResult JSONPromociones(string id)
        {
            using (DBEnt db = new DBEnt())
            {
                var user = db.HomePromociones.ToList<HomePromociones>();

                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult ProductosFinancierosJSON(string id)
        {
            using (DBEnt db = new DBEnt())
            {
                var user = db.HomeProductosFinancieros.ToList<HomeProductosFinancieros>();

                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InfoItemCarrousel(int id)
        {

            using (DBEnt db = new DBEnt())
            {
                var item = db.HomeCarrouselItems.Where(u => u.id.Equals(id)).FirstOrDefault();

                return Json(item, JsonRequestBehavior.AllowGet);
            }

        }


        // avisos de contacto
        [Authorize]
        [HttpPost]
        public ActionResult InsertContactoAviso(HttpPostedFileBase files,
            string head, string th1, string th2, string destino, string tipo,
            string id_userCreated,  
            string direccion, string telefono, string email)
        {

            var fx = files;
 
            if (fx == null)
            {

                var img = "sin imagen";
                using (DBEnt db = new DBEnt())
                {

                    var car = new Contactos()
                    {
                        head = head,
                        th1 = th1,
                        th2 = th2,
                        img = img,
                        destino = destino,
                        tipo = tipo,
                        oculto = "show",
                        direccion = direccion,
                        telefono = telefono,
                        email = email

                    };
                    db.Contactos.Add(car);
                    //db..add(cat);
                    db.SaveChanges();
                }

               
            }
            else {          

                DateTime dtime = new DateTime();
                dtime = DateTime.Now;

                int idint = RandomNumber(1, 1000000000);
                string id = idint.ToString();
                string filename = "" + "_" + id + "_" +
                                   String.Format("{0:d}", dtime) + files.FileName;
                filename = filename.Replace("/", "_");

                try
                {
                    fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\ContactoImagenes", filename));
                    fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\ContactoImagenes", filename));

                }
                catch (Exception)
                {

                    throw;
                }

                var img = "/WebFiles/ContactoImagenes/" + filename;

                var ddt = String.Format("{0:d}", dtime);



                using (DBEnt db = new DBEnt())
                {

                    var car = new Contactos()
                    {
                        head = head,
                        th1 = th1,
                        th2 = th2,
                        img = img,
                        destino = destino,
                        tipo = tipo,
                        oculto = "show",
                        direccion = direccion,
                        telefono = telefono,
                        email = email

                    };
                    db.Contactos.Add(car);
                    //db..add(cat);
                    db.SaveChanges();
                }

            }
            var action = "Insert";
            var comments = "Carrousel Item ingresado por el usuario : ";

            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("DireccionAdministracion", "DashboardAdmin");
        }


        [Authorize]
        [HttpPost]

        public ActionResult DeleteContactoAviso(int id,string id_userCreated) {


            using (DBEnt db = new DBEnt())
            {
                var q = db.Contactos.Where(u => u.id.Equals(id)).ToList().FirstOrDefault();                
                    db.Contactos.Remove(q);  
                    System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + q.img);
                    System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + q.img);              
                db.SaveChanges();
            }
            

            var action = "Delete";
            var comments = "Info de contacto eliminada por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("DireccionAdministracion", "DashboardAdmin");
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateContactoAviso(int id, string id_userCreated,
            string head,string th1, string th2, string direccion, string telefono,
            string email,string destino, string oculto,string tipo, HttpPostedFileBase files
            ) {

            var fx =  files;
            if (fx == null)
            {
                using (DBEnt db = new DBEnt())
                {
                    var x = db.Contactos.ToList<Contactos>().Where(u => u.id.Equals(id)).FirstOrDefault();
                    x.destino = destino;
                    x.direccion = direccion;
                    x.email = email;
                    x.oculto = oculto;
                    x.head = head;
                    x.th1 = th1;
                    x.th2 = th2;
                    x.tipo = tipo;
                    x.telefono = telefono;
                    db.SaveChanges();                                        
                }

            }
            else
            {

                using (DBEnt db = new DBEnt())
                {
                    var x = db.Contactos.ToList<Contactos>().Where(u => u.id.Equals(id)).FirstOrDefault();
                    var filedelete = x.img;
                    //System.IO.File.Delete(filedelete);


                    DateTime dtime = new DateTime();
                    dtime = DateTime.Now;

                    int idint = RandomNumber(1, 1000000000);
                    string id_ = idint.ToString();
                    string filename = "" + "_" + id_ + "_" +
                                       String.Format("{0:d}", dtime) +  files.FileName;
                    filename = filename.Replace("/", "_");

                    System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\" + x.img);
                    System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\" + x.img);
                    try
                    {


     


                        fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\DashboardAdminFS_2\DashboardAdminFS_2\WebFiles\ContactoImagenes", filename));
                        fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\WebFS\WebFS\WebFiles\ContactoImagenes", filename));
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    var img = "/WebFiles/ContactoImagenes/" + filename;

                    x.destino = destino;
                    x.direccion = direccion;
                    x.email = email;
                    x.oculto = oculto;
                    x.head = head;
                    x.th1 = th1;
                    x.th2 = th2;
                    x.tipo = tipo;
                    x.telefono = telefono;
                    x.img = img;
 

 
                    db.SaveChanges();

 
                }
            }

            var action = "Update";
            var comments = "Info de contacto actualizado por el usuario : ";
            InsertInBitacora(id_userCreated, action, comments + id_userCreated);
            return RedirectToAction("DireccionAdministracion", "DashboardAdmin");
        }


        public JsonResult GetContacto_Contacto( )
        {

            using (DBEnt db = new DBEnt())
            {
                var item = db.Contactos.Where(u => u.tipo.Equals("Contacto")).ToList();

                return Json(item, JsonRequestBehavior.AllowGet);
            }

        }



        public JsonResult GetContacto_Bolsa( )
        {

            using (DBEnt db = new DBEnt())
            {
                var item = db.Contactos.Where(u => u.tipo.Equals("Bolsa")).ToList();

                return Json(item, JsonRequestBehavior.AllowGet);
            }

        }
        
        
        //aviso de contacto end


        [Route("CheckRoleExist/{n}")]
        public JsonResult CheckRoleExist(string n)
        {
            using (DBEnt db = new DBEnt())
            {
                var role = db.AspNetRoles.ToList<AspNetRoles>().Where(u => u.Name.Equals(n)).FirstOrDefault();
                if (role != null)
                {
                    return Json(role.Name, JsonRequestBehavior.AllowGet);
                }
                else {
                    var valido = "Nombre válido";
                    return Json(valido, JsonRequestBehavior.AllowGet);
                }

            }
        }





        //Error AREA 


        public JsonResult JSONUsers()
        {
            using (DBEnt db = new DBEnt())
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    var user = db.AspNetUsers.ToList();
                    return Json(user, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public JsonResult BarrasJSON()
        {
            using (DBEnt db = new DBEnt())
            {
                var user = db.HomeBarras.ToList<HomeBarras>();

                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult MisionJSON()
        {
            using (DBEnt db = new DBEnt())
            {
                var mision = db.HomeMision.ToList<HomeMision>();

                return Json(mision, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult VisionJSON()
        {
            using (DBEnt db = new DBEnt())
            {
                var vision = db.HomeVision.ToList<HomeVision>();

                return Json(vision, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CounterJSON()
        {
            using (DBEnt db = new DBEnt())
            {
                var vision = db.HomeCounters.ToList<HomeCounters>();

                return Json(vision, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DatosExtraJSON()
        {
            using (DBEnt db = new DBEnt())
            {
                var datos = db.HomeDatosExtra.ToList<HomeDatosExtra>();

                return Json(datos, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CountersJSON()
        {
            using (DBEnt db = new DBEnt())
            {
                var datos = db.HomeCounters.ToList<HomeCounters>();
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ContactoDatosJson()
        {
            using (DBEnt db = new DBEnt())
            {
                var datos = db.HomeContactoDatos.ToList<HomeContactoDatos>();
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult BeneficiosJson()
        {
            using (DBEnt db = new DBEnt())
            {
                var datos = db.HomeBeneficios.ToList<HomeBeneficios>();
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsonCodigoEtico()
        {
            using (DBEnt db = new DBEnt())
            {
                var datos = db.HomeCodigoDeEtica.ToList<HomeCodigoDeEtica>();
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsonValores()
        {
            using (DBEnt db = new DBEnt())
            {
                var datos = db.HomeValores.ToList<HomeValores>();
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CookiesJson()
        {
            using (DBEnt db = new DBEnt())
            {
                var datos = db.HomeAnuncioCookies.ToList<HomeAnuncioCookies>();
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsonRoles() {

            using (DBEnt db = new DBEnt())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var datos = db.AspNetRoles.ToList<AspNetRoles>();
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult UserInfoJSON(string id)
        {
            using (DBEnt db = new DBEnt())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var user = db.AspNetUsers.ToList<AspNetUsers>().Where(u => u.Id.Equals(id)).FirstOrDefault();

                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("UserInfoRolesJSON/{id}")]
        public JsonResult UserInfoRolesJSON(string id)
        {
            using (DBEnt db = new DBEnt())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var user = db.AspNetUserRoles.ToList<AspNetUserRoles>().Where(u => u.UserId.Equals(id)).FirstOrDefault();
                if (user == null)
                {

                    ErrorModel user_err = new ErrorModel();
                    user_err.Mensaje = "usuario sin role";
                    user_err.UserId = id.ToString();

                    string output = JsonConvert.SerializeObject(user_err);

                    return Json(user_err, JsonRequestBehavior.AllowGet);
                }
                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("RolesInfoJson/{id}")]
        public JsonResult RolesInfoJson(string id)
        {
            using (DBEnt db = new DBEnt())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var user = db.AspNetRoles.ToList<AspNetRoles>().Where(u => u.Id.Equals(id)).FirstOrDefault();

                if (user == null)
                {

                    ErrorModel user_err = new ErrorModel();
                    user_err.Mensaje = "usuario sin role";


                    string output = JsonConvert.SerializeObject(user_err);

                    return Json(user_err, JsonRequestBehavior.AllowGet);
                }
                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }
        //END GET JSON LIST



 
 


        public ActionResult UpdateItemCarrousel(EditCarrouselItem data)
        {

            switch (data.h2tag)
            {
                case null:
                    data.h2tag = "";
                    break;
                default:
                    break;
            }

            switch (data.ptag)
            {
                case null:
                    data.ptag = "";
                    break;
                default:
                    break;
            }

            if (data.files != null)
            {

                var fx = data.files;
                using (DBEnt db = new DBEnt())
                {

                    var carrousel = db.HomeCarrouselItems.ToList<HomeCarrouselItems>().Where(u => u.id.Equals(data.id)).FirstOrDefault();





                    var filedelete = carrousel.img;
                    System.IO.File.Delete(@"C:\Users\EjeDesarrolloCS-162\source\repos\FSDashboardAdmin\FSDashboardAdmin\" + filedelete);


                    DateTime dtime = new DateTime();
                    dtime = DateTime.Now;

                    int idint = RandomNumber(1, 1000000000);
                    string id_ = idint.ToString();
                    string filename = "" + "_" + id_ + "_" +
                                       String.Format("{0:d}", dtime) + data.files.FileName;
                    filename = filename.Replace("/", "_");


                    try
                    {
                        fx.SaveAs(Path.Combine(@"C:\Users\EjeDesarrolloCS-162\source\repos\FSDashboardAdmin\FSDashboardAdmin\FIles\Creditos", filename));
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    var img = "/files/Creditos/" + filename;

                    carrousel.h2tag = data.h2tag;
                    carrousel.ptag = data.ptag;
                    carrousel.img = img;

                    var action = "UPDATE";
                    var comments = "Carrousel actualizado por el usuario : ";

                    InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);

                    db.SaveChanges();

                    return RedirectToAction("carrousel", "dashboardadmin", new { id = data.id });




                }
            }
            else
            {

                using (DBEnt db = new DBEnt())
                {
                    var carrousel = db.HomeCarrouselItems.ToList<HomeCarrouselItems>().Where(u => u.id.Equals(data.id)).FirstOrDefault();

                    carrousel.h2tag = data.h2tag;
                    carrousel.ptag = data.ptag;

                    var action = "UPDATE";
                    var comments = "Carrousel actualizado por el usuario : ";

                    InsertInBitacora(data.id_userCreated, action, comments + data.id_userCreated);

                    db.SaveChanges();

                    return RedirectToAction("carrousel", "dashboardadmin", new { id = data.id });

                }

            }
 
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditValores(int id_valor,string textoHead, string txtEditorEdit,string id_userCreated) {

            using (DBEnt db = new DBEnt())
            {
                var valor = db.HomeValores.ToList<HomeValores>().Where(u => u.id.Equals(id_valor)).FirstOrDefault();

                valor.texto = txtEditorEdit;
                valor.textoHead = textoHead;

                var action = "UPDATE";
                var comments = "Valores actualizado por el usuario : ";

                InsertInBitacora(id_userCreated, action, comments + id_userCreated);

                db.SaveChanges();

                return RedirectToAction("EditValores", "dashboardadmin");

            }
           
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCodigoDeEtica(int id_valor, string textoHead, string txtEditorEdit, string id_userCreated)
        {

            using (DBEnt db = new DBEnt())
            {
                var valor = db.HomeCodigoDeEtica.ToList<HomeCodigoDeEtica>().Where(u => u.id.Equals(id_valor)).FirstOrDefault();

                valor.texto = txtEditorEdit;
                valor.textoHead = textoHead;

                var action = "UPDATE";
                var comments = "Codigo de ética actualizado por el usuario : ";

                InsertInBitacora(id_userCreated, action, comments + id_userCreated);

                db.SaveChanges();

                return RedirectToAction("EditCodigoDeEtica", "dashboardadmin");

            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditBeneficios(int id_valor, string icono, string txtEditorEdit, string id_userCreated)
        {

            using (DBEnt db = new DBEnt())
            {
                var valor = db.HomeBeneficios.ToList<HomeBeneficios>().Where(u => u.id.Equals(id_valor)).FirstOrDefault();

                valor.texto = txtEditorEdit;
                valor.icono = icono;
                

                var action = "UPDATE";
                var comments = "Beneficios actualizado por el usuario : ";

                InsertInBitacora(id_userCreated, action, comments + id_userCreated);

                db.SaveChanges();

                return RedirectToAction("EditCodigoDeEtica", "dashboardadmin");

            }

        }




        //
    }
}   