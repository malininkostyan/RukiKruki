using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class ClientOrdersViewModel
    {
        public string ClientName { get; set; }

        public string DateCreateOrder { get; set; }

        public List<OrderTOViewModel> OrderTOs { get; set; }

        public decimal TotalSum { get; set; }

        public string StatusOrder { get; set; }
    }
}
