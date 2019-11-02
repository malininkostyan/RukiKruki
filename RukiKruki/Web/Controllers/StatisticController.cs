using DAL.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;

namespace Web.Controllers
{
    public class StatisticController : Controller
    {
        readonly IStatisticService service = Globals.StatisticService;
        // GET: Statisics
        public ActionResult Index()
        {
            ViewBag.Service = service;
            return View();
        }

        public ActionResult SaveStatistic()
        {
            string fileName = "D:\\reports\\statistic.pdf";
            Globals.ReportService.PrintStatistic(Globals.AuthClient.Id, fileName);
            return RedirectToAction("Index");
        }

        public ActionResult CreateChart()
        {
            var list = service.GetTOStatistic();
            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());

            chart.Series.Add(new Series("Data"));
            chart.Series["Data"].ChartType = SeriesChartType.Column;
            chart.Series["Data"]["PieLabelStyle"] = "Outside";
            chart.Series["Data"]["PieLineColor"] = "Black";
            chart.Series["Data"].Points.DataBindXY(
                list.Select(data => data.TOName.ToString()).ToArray(),
                list.Select(data => data.TOCount).ToArray());

            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms, ChartImageFormat.Png);

            ms.Position = 0;
            System.IO.File.WriteAllBytes(@"D:\\reports\diagram.png", ms.ToArray());

            return File(ms.ToArray(), "image/png");
        }
    }
}