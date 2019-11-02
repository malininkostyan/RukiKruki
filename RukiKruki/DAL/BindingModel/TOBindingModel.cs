using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BindingModel
{
    public class TOBindingModel
    {
        public int Id { get; set; }

        public string TOName { get; set; }

        public int Price { get; set; }

        public virtual List<TO_DetailBindingModel> TODetails { get; set; }
    }
}
