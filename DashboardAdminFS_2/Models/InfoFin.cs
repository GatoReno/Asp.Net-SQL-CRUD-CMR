    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardAdminFS_2.Models
{
    public class InfoFin
    {
        public HttpPostedFileBase files { get; set; }
        public string id_userCreated { get; set; }
        public string tipo { get; set; }

        public DateTime datecorresponde { get; set; }

        
    }
}