using System.ComponentModel;

namespace DAL.ViewModel
{
    public class CarViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [DisplayName("VIN-номер автомобиля")]
        public string VIN { get; set; }

        [DisplayName("Марка автомобиля")]
        public string Brand { get; set; }

        [DisplayName("Модель автомобиля")]
        public string Model { get; set; }

        [DisplayName("Пробег автомобиля")]
        public int Mileage { get; set; }
    }
}
