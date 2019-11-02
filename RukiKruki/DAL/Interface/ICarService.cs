using DAL.BindingModel;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ICarService
    {
        List<CarViewModel> GetList();

        CarViewModel GetElement(int id);

        void AddElement(CarBindingModel model);

        void UpdateElement(CarBindingModel model);

        void DeleteElement(int id);

        List<CarViewModel> GetClientCars(int clientId);
    }
}
