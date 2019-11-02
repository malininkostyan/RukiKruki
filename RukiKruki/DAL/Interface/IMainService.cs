using DAL.BindingModel;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IMainService
    {
        List<OrderViewModel> GetList();

        List<OrderViewModel> GetClientOrders(int clientId);

        OrderViewModel GetElement(int id);

        void CreateOrder(OrderBindingModel model);

        void TakeOrderInWork(OrderBindingModel model);

        void FinishOrder(OrderBindingModel model);

        void PayOrder(OrderBindingModel model);

        void ReserveOrder(OrderBindingModel model);

        void SaveDataBaseClient();

        void SaveDataBaseAdmin();
    }
}
