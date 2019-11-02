using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BindingModel
{
    public class RequestBindingModel
    {
        public int Id { get; set; }

        public DateTime DateCreate { get; set; }

        public List<DetailRequestBindingModel> DetailRequests { get; set; }
    }
}
