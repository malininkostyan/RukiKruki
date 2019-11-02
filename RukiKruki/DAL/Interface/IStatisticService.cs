using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IStatisticService
    {
        (string name, int count) GetMostPopularTO();

        decimal GetAverageCustomerCheck(int clientId);

        int GetClientTOsCount(int clientId);

        (string name, int count) GetPopularTOClient(int clientId);

        decimal GetAverageCheck();

        List<TOCountViewModel> GetTOStatistic();
    }
}
