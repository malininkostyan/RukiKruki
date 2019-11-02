using DAL.BindingModel;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ITOService
    {
        List<TOViewModel> GetList();

        List<TOViewModel> GetFilteredList();

        TOViewModel GetElement(int id);

        void AddElement(TOBindingModel model);

        void UpdateElement(TOBindingModel model);

        void DeleteElement(int id);
    }
}
