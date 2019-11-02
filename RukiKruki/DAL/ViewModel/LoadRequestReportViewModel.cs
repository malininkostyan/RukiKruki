using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class LoadRequestReportViewModel
    {
        public string DateCreate { get; set; }
        public IEnumerable<Tuple<string, int>> Details { get; set; }
    }
}
