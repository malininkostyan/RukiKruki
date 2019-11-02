using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BindingModel
{
    public class OrderBindingModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public decimal TotalSum { get; set; }

        public virtual List<OrderTOBindingModel> OrderTOs { get; set; }
    }
}
