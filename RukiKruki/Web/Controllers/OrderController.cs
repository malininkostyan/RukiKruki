using DAL.BindingModel;
using DAL.Interface;
using DAL.ViewModel;
using DataBase.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMainService service = Globals.MainService;
        private readonly ITOService _TOService = Globals.TOService;
        private readonly IStatisticService statistic = Globals.StatisticService;

        // GET: Vouchers
        public ActionResult Index()
        {
            if (Session["Order"] == null)
            {
                var order = new OrderViewModel { OrderTOs = new List<OrderTOViewModel>() };
                Session["Order"] = order;
            }
            ViewBag.Service = statistic;
            return View((OrderViewModel)Session["Order"]);
        }

        public ActionResult Reserve()
        {
            return View();
        }

        public ActionResult AddTO()
        {
            var _TOs = new SelectList(_TOService.GetList(), "Id", "TOName");
            ViewBag.TOs = _TOs;
            return View();
        }

        [HttpPost]
        public ActionResult AddTOPost()
        {
            var order = (OrderViewModel)Session["Order"];
            var _TO = new OrderTOViewModel
            {
                TOId = int.Parse(Request["Id"]),
                TOName = _TOService.GetElement(int.Parse(Request["Id"])).TOName,
                Amount = int.Parse(Request["Amount"])
            };
            order.OrderTOs.Add(_TO);
            Session["Order"] = order;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateOrderPost()
        {
            var order = (OrderViewModel)Session["Order"];
            var orderTOs = new List<OrderTOBindingModel>();
            for (int i = 0; i < order.OrderTOs.Count; ++i)
            {
                orderTOs.Add(new OrderTOBindingModel
                {
                    Id = order.OrderTOs[i].Id,
                    OrderId = order.OrderTOs[i].OrderId,
                    TOId = order.OrderTOs[i].TOId,
                    Amount = order.OrderTOs[i].Amount
                });
            }

            service.CreateOrder(new OrderBindingModel
            {
                ClientId = Globals.AuthClient.Id,
                TotalSum = orderTOs.Sum(rec => rec.Amount * _TOService.GetElement(rec.TOId).Price),
                OrderTOs = orderTOs
            });
            Session.Remove("Order");
            return RedirectToAction("Index", "Orders");
        }

        [HttpPost]
        public ActionResult ReservePost()
        {
            var order = (OrderViewModel)Session["Order"];
            order.DateCreate = DateTime.Now.ToShortDateString();

            if (Globals.DbContext.Orders.Any())
            {
                order.Id = Globals.DbContext.Orders.Max(rec => rec.Id) + 1;
            }
            else
            {
                order.Id = 1;
            }

            order.ClientId = Globals.AuthClient.Id;
            order.ClientFIO = Globals.AuthClient.ClientFIO;

            var orderTOs = new List<OrderTOBindingModel>();
            for (int i = 0; i < order.OrderTOs.Count; ++i)
            {
                orderTOs.Add(new OrderTOBindingModel
                {
                    Id = order.OrderTOs[i].Id,
                    OrderId = order.OrderTOs[i].OrderId,
                    TOId = order.OrderTOs[i].TOId,
                    Amount = order.OrderTOs[i].Amount
                });
            }

            service.ReserveOrder(new OrderBindingModel
            {
                ClientId = Globals.AuthClient.Id,
                TotalSum = orderTOs.Sum(rec => rec.Amount * _TOService.GetElement(rec.TOId).Price),
                OrderTOs = orderTOs
            });

            order.TotalSum = orderTOs.Sum(rec => rec.Amount * _TOService.GetElement(rec.TOId).Price);

            string basePathReports = "D:\\reports\\";

            string wordFile = basePathReports + "reserve.doc";
            string excelFile = basePathReports + "reserve.xls";

            Globals.ReportService.SaveClientReserveExcel(order, excelFile);
            Globals.ReportService.SaveClientReserveWord(order, wordFile);

            var files = new List<string>
            {
                wordFile,
                excelFile
            };

            MailService.SendEmail(Globals.AuthClient.Mail, "Оповещение по заказам",
                $"Заказ №{order.Id} от {order.DateCreate} зарезервирован успешно", files);

            Session.Remove("Order");
            return RedirectToAction("Index", "Orders");
        }
    }
}