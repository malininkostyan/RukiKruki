using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [DisplayName("ФИО Клиента")]
        public string ClientFIO { get; set; }

        public List<OrderTOViewModel> OrderTOs { get; set; }

        [DisplayName("Сумма")]
        public decimal TotalSum { get; set; }

        [DisplayName("Статус заказа")]
        public string StatusOrder { get; set; }

        [DisplayName("Дата создания заказа")]
        public string DateCreate { get; set; }

        [DisplayName("Дата завершения заказа")]
        public string DateImplement { get; set; }
    }
}
