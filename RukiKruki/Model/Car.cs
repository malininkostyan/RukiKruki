using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Car
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        [Required]
        public string VIN { get; set; }

        [DataMember]
        [Required]
        public string Brand { get; set; }

        [DataMember]
        [Required]
        public string Model { get; set; }

        [DataMember]
        [Required]
        public int Mileage { get; set; }

        public virtual Client Client { get; set; }

    }
}
