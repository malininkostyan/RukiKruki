using DAL.BindingModel;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRequestService
    {
        List<RequestViewModel> GetList();

        RequestViewModel GetElement(int id);

        void AddElement(RequestBindingModel model);

        void DeleteElement(int id);

        LoadRequestReportViewModel GetDetailsRequest(int id);

        void SaveRequestToWord(LoadRequestReportViewModel request, string fileName);

        void SaveRequestToExcel(LoadRequestReportViewModel request, string fileName);
    }
}
