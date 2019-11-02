using DAL.BindingModel;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IClientService
    {
        List<ClientViewModel> GetList();

        ClientViewModel GetElement(int id);

        void AddElement(ClientBindingModel model);

        void UpdateElement(ClientBindingModel model);

        void DeleteElement(int id);
    }
}
