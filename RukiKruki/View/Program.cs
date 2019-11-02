﻿using DAL.Interface;
using DataBase;
using DataBase.Implementations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace View
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer.RegisterType<DbContext, RukiKrukiDbContext>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IClientService, ClientServiceDB>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IDetailService, DetailServiceDB>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<ITOService, TOServiceDB>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IMainService, MainServiceDB>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<ICarService, CarServiceDB>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IReportService, ReportServiceDB>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IRequestService, RequestServiceDB>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IStatisticService, StatisticServiceDB>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

    }
}