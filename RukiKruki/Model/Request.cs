using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Request
    {
        public int Id { get; set; }

        public DateTime DateCreate { get; set; }

        [ForeignKey("RequestId")]
        public virtual List<RequestDetail> DetailRequests { get; set; }
    }
}
