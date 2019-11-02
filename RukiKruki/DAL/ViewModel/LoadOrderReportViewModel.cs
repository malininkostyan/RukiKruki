using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class LoadOrderReportViewModel
    {
        public string DateCreate { get; set; }

        public int OrderId { get; set; }

        public int TOId { get; set; }

        public string TOName { get; set; }

        public int TOAmount { get; set; }

        public List<TO_DetailViewModel> TODetails { get; set; }
    }
}
