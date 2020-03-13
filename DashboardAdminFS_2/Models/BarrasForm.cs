using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardAdminFS_2.Models
{
    public class BarrasForm
    {

        public string id_userCreated { set; get; }

        public string text { get; set; }
        public Nullable<int> valor { get; set; }
        public string color { get; set; }
    }
}