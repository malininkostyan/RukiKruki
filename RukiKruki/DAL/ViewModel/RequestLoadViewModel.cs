using System;
using System.Collections.Generic;

namespace DAL.ViewModel
{
    public class RequestLoadViewModel
    {
        public string DateRequest { get; set; }
        public IEnumerable<Tuple<string, int>> Details { get; set; }
    }
}
