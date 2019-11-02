using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BindingModel
{
    public class OrderTOBindingModel
    {
        public int Id { get; set; }

        public int TOId { get; set; }

        public int OrderId { get; set; }

        public int Amount { get; set; }
    }
}
