using System.ComponentModel;

namespace DAL.ViewModel
{
    public class OrderTOViewModel
    {
        public int Id { get; set; }

        public int TOId { get; set; }

        public int OrderId { get; set; }

        [DisplayName("TO")]
        public string TOName { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }
    }
}
