using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardAdminFS_2.Models
{
    public class EditProductoFin
    {
        public HttpPostedFileBase files { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string descripcion { get; set; }
        public string montoCredito { get; set; }
        public int plazoMinimo { get; set; }
        public int plazoMaximo { get; set; }
        public string frecuenciasDePago { get; set; }
        public string esquemaDeCobro { get; set; }
        public string requisitos { get; set; }
        public string tasaDeInteres { get; set; }
        public string CAT { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime lastupdate { get; set; }
        public string id_userCreated { get; set; }
        public string tipocredito { get; set; }
        public string tencologiaCrediticia { get; set; }
        public int lapMaxSemanas { get; set; }
        public int lapMinSemanas { get; set; }
        public string tipoRetornoPagoSemanal { get; set; }
        public string tipoRetornoPagoCatorcenal { get; set; }
        public string tipoRetornoPago28Dias { get; set; }
        public string otrasComisones { get; set; }
        public int numeroDeIntegrantes { get; set; }
        public string grantiaPersonalAval { get; set; }
        public string edad { get; set; }
        public string soloclientes { get; set; }
    }
}