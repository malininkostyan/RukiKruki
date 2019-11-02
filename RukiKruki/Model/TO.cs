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
    public class TO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string TOName { get; set; }

        [Required]
        [DataMember]
        public int Price { get; set; }

        [ForeignKey("TOId")]
        public virtual List<TO_Detail> TO_Details { get; set; }

        [ForeignKey("TOId")]
        public virtual List<OrderTO> OrderTOs { get; set; }
    }
}
