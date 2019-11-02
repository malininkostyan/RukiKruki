using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class Detail
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string DetailName { get; set; }

        [DataMember]
        [Required]
        public int Amount { get; set; }

        [DataMember]
        [Required]
        public int Reserve { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<TO_Detail> TODetails { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<RequestDetail> RequestDetails { get; set; }
    }
}
