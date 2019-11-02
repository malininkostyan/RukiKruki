using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DAL.ViewModel
{
    public class RequestViewModel
    {
        public int Id { get; set; }

        [DisplayName("Дата оформления")]
        public DateTime DateCreate { get; set; }

        public List<RequestDetailViewModel> DetailRequests { get; set; }
    }
}
