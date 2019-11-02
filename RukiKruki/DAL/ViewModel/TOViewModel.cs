using System.Collections.Generic;
using System.ComponentModel;

namespace DAL.ViewModel
{
    public class TOViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название ТО")]
        public string TOName { get; set; }

        [DisplayName("Цена ТО")]
        public decimal Price { get; set; }

        public List<TO_DetailViewModel> TODetails { get; set; }
    }
}
