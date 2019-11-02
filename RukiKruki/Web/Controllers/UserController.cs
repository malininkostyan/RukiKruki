﻿using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        readonly IStatisticService service = Globals.StatisticService;
        // GET: User
        public ActionResult Index()
        {
            ViewBag.User = Globals.AuthClient;
            ViewBag.Service = service;
            return View();
        }

        public ActionResult SaveDataBaseClient()
        {
            Globals.MainService.SaveDataBaseClient();
            return RedirectToAction("Index");
        }
    }
}