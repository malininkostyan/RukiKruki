using DAL.BindingModel;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IReportService
    {
        List<LoadRequestReportViewModel> GetDetailsRequest(ReportBindingModel model);

        List<LoadOrderReportViewModel> GetDetailsOrder(ReportBindingModel model);

        void SaveDetailsReport(List<LoadRequestReportViewModel> DetailsRequest, List<LoadOrderReportViewModel> DetailsOrder, string fileName, ReportBindingModel model);

        List<ClientOrdersViewModel> GetClientOrders(ReportBindingModel model, int clientId);

        void SaveClientOrders(ReportBindingModel model, int clientId);

        void SaveClientReserveWord(OrderViewModel model, string fileName);

        void SaveClientReserveExcel(OrderViewModel model, string fileName);

        void PrintStatistic(int clientId, string fileName);
    }
}
