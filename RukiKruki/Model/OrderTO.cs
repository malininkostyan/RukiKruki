using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class OrderTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int TOId { get; set; }

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        [Required]
        public int Amount { get; set; }

        public virtual TO TO { get; set; }

        public virtual Order Order { get; set; }
    }
}
