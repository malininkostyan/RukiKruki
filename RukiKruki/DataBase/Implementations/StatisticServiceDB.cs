using DAL.Interface;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Implementations
{
    public class StatisticServiceDB : IStatisticService
    {
        private readonly RukiKrukiDbContext context;

        public StatisticServiceDB(RukiKrukiDbContext context)
        {
            this.context = context;
        }

        public (string name, int count) GetMostPopularTO()
        {
            var most = context.OrderTOs
                .GroupBy(rec => rec.TOId)
                .Select(rec => new { Id = rec.Key, Total = rec.Sum(x => x.Amount) })
                .OrderByDescending(rec => rec.Total)
                .First();

            var name = context.TOs.FirstOrDefault(rec => rec.Id == most.Id)?.TOName;

            var count = most.Total;

            return (name, count);
        }

        public int GetClientTOsCount(int clientId)
        {
            int clientTOs = context.Orders
                .Count(order => order.ClientId == clientId);

            if (clientTOs != 0)
            {
                return context.Orders
                    .Where(order => order.ClientId == clientId)
                    .Sum(order => order.OrderTOs.Sum(x => x.Amount));
            }
            else
            {
                return 0;
            }
        }

        public decimal GetAverageCustomerCheck(int clientId)
        {
            int clientTOs = context.Orders
                .Count(order => order.ClientId == clientId);

            if (clientTOs != 0)
            {
                return context.Orders
                    .Where(order => order.ClientId == clientId)
                    .Average(order => order.TotalSum);
            }
            else
            {
                return 0;
            }
        }


        public (string name, int count) GetPopularTOClient(int clientId)
        {
            var most = context.OrderTOs
                .Where(rec => rec.Order.ClientId == clientId)
                .GroupBy(rec => rec.TOId)
                .Select(rec => new { Id = rec.Key, Total = rec.Sum(x => x.Amount) })
                .OrderByDescending(rec => rec.Total)
                .FirstOrDefault();

            if (most != null)
            {
                var name = context.TOs.FirstOrDefault(rec => rec.Id == most.Id)?.TOName;

                var count = most.Total;

                return (name, count);
            }
            else
            {
                return (name: null, count: 0);
            }
        }

        public decimal GetAverageCheck()
        {
            return context.Orders.Average(order => order.TotalSum);
        }

        public List<TOCountViewModel> GetTOStatistic()
        {
            var data = new List<TOCountViewModel>();

            var _TOs = context.OrderTOs
                .GroupBy(rec => context.TOs.FirstOrDefault(r => r.Id == rec.TOId).TOName)
                .Select(rec => new { Name = rec.Key, Total = rec.Sum(x => x.Amount) })
                .OrderByDescending(rec => rec.Total);

            foreach (var _TO in _TOs)
            {
                data.Add(new TOCountViewModel
                {
                    TOName = _TO.Name,
                    TOCount = _TO.Total
                });
            }

            return data;
        }
    }
}