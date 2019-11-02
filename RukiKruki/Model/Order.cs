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
    public class Order
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        [Required]
        public decimal TotalSum { get; set; }

        [DataMember]
        public OrderStatus OrderStatus { get; set; }

        [DataMember]
        [Required]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public DateTime? DateImplement { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey("OrderId")]
        public virtual List<OrderTO> OrderTOs { get; set; }
    }
}
