using DAL.Interface;
using DAL.ViewModel;
using DataBase;
using DataBase.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class Globals
    {
        public static RukiKrukiDbContext DbContext { get; } = new RukiKrukiDbContext();
        public static IClientService ClientService { get; } = new ClientServiceDB(DbContext);
        public static ITOService TOService { get; } = new TOServiceDB(DbContext);
        public static ICarService CarService { get; } = new CarServiceDB(DbContext);
        public static IMainService MainService { get; } = new MainServiceDB(DbContext);
        public static ClientViewModel AuthClient { get; set; } = null;
        public static IReportService ReportService { get; } = new ReportServiceDB(DbContext);
        public static IRequestService RequestService { get; } = new RequestServiceDB(DbContext);
        public static IStatisticService StatisticService { get; } = new StatisticServiceDB(DbContext);
        public static IDetailService DetailService { get; } = new DetailServiceDB(DbContext);
        public static ClientOrders ModelReport { get; set; }
    }
}