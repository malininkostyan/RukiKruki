using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class ClientOrders
    {
        public List<OrderViewModel> Orders { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}