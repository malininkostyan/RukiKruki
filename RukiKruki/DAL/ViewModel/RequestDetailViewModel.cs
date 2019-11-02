using System.ComponentModel;

namespace DAL.ViewModel
{
    public class RequestDetailViewModel
    {
        public int Id { get; set; }

        public int DetailId { get; set; }

        public int RequestId { get; set; }

        [DisplayName("Количество деталей")]
        public int Amount { get; set; }
    }
}
