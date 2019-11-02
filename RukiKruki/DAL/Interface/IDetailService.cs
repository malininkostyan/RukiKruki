using DAL.BindingModel;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IDetailService
    {
        List<DetailViewModel> GetList();

        DetailViewModel GetElement(int id);

        void AddElement(DetailBindingModel model);

        void UpdateElement(DetailBindingModel model);

        void DeleteElement(int id);
    }
}
