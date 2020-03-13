using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardAdminFS_2.Models
{
    public class CarrouselItem
    {
        public HttpPostedFileBase files { get; set; }

        public string h2tag { set; get; }

        public string ptag { set; get; }

        public string id_userCreated { set; get; }
    }
}