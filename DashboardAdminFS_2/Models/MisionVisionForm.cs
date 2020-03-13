using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardAdminFS_2.Models
{
    public class MisionVisionForm
    {
        public HttpPostedFileBase files { get; set; }
        public string text { get; set; }

        public string id_userCreated { get; set; }
    }
}