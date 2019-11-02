using System.ComponentModel;

namespace DAL.ViewModel
{
    public class DetailViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название детали")]
        public string DetailName { get; set; }

        [DisplayName("Количество деталей")]
        public int Amount { get; set; }
    }
}
