using System.ComponentModel;

namespace DAL.ViewModel
{
    public class TO_DetailViewModel
    {
        public int Id { get; set; }

        public int TOId { get; set; }

        public int DetailId { get; set; }

        [DisplayName("Деталь")]
        public string DetailName { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }
    }
}
