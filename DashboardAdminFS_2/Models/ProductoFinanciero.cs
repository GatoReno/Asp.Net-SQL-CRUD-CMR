using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardAdminFS_2.Models
{
    public class ProductoFinanciero
    {
        public HttpPostedFileBase files { get; set; }

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
        public string CATDescripcion { get; set; }
        public string id_userCreated { get; set; }
        public string tipocredito { set; get; }
        public string tencologiaCrediticia { set; get; }
        public string tipoRetornoPagoSemanal { set; get; }
        public string tipoRetornoPagoCatorcenal { set; get; }
        public string tipoRetornoPago28Dias { set; get; }
        public string otrasComisones { set; get; }
        public int lapMaxSemanas { set; get; }
        public int lapMinSemanas { set; get; }
        public int numeroDeIntegrantes { set; get; }
        public string grantiaPersonalAval { set; get; }
        public string edad { set; get; }

        public string interes { set; get; }
        public string costoTotalAnualCredito { set; get; }
        


    }
}