using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class TOController : Controller
    {
        // GET: TO
        public ITOService Service = Globals.TOService;

        // GET: TOs
        public ActionResult Index()
        {
            return View(Service.GetList());
        }

        public ActionResult Filter()
        {
            return View(Service.GetFilteredList());
        }

        // GET: TOs/Details/5
        public ActionResult Details(int id)
        {
            var _TO = Service.GetElement(id);

            if (_TO == null)
            {
                return HttpNotFound();
            }
            return View(_TO);
        }
    }
}