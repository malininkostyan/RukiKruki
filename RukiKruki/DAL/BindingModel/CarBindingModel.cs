using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BindingModel
{
    public class CarBindingModel
    {
        public int Id { get; set; }

        public string VIN { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Mileage { get; set; }

        public int ClientId { get; set; }
    }
}
