using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardAdminFS_2.Models
{
    public class EditCarrouselItem
    {
        public HttpPostedFileBase files { get; set; }
        public string h2tag { get; set; }
        public string ptag { get; set; }

        public int id { set; get; }
        public string id_userCreated { set; get; }
    }
}