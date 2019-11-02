using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BindingModel
{
    public class TO_DetailBindingModel
    {
        public int Id { get; set; }

        public int TOId { get; set; }

        public int DetailId { get; set; }

        public int Amount { get; set; }
    }
}
