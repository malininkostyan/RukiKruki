﻿using DAL.BindingModel;
using DAL.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ClientsController : Controller
    {
        public IClientService Service = Globals.ClientService;

        // GET: Clients
        public ActionResult Index()
        {
            return View(Service.GetList());
        }

        public ActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Auth([Bind(Include = "Login, Password")] Client client)
        {
            if (Service.GetList().Any(rec => rec.Login == client.Login && rec.Password == client.Password))
            {
                var authClient = Service.GetList().FirstOrDefault(cl => cl.Login == client.Login);
                Globals.AuthClient = authClient;
                return RedirectToAction("Index", "Orders");
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Неверные данные');</script>");
            }

            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientFIO,Login,Password,Mail")] Client client)
        {
            if (ModelState.IsValid)
            {
                var clientDB = Service.GetList().FirstOrDefault(rec =>
                        rec.ClientFIO == client.ClientFIO ||
                        rec.Login == client.Login ||
                        rec.Mail == client.Mail);

                if (clientDB != null)
                    return Content("<script language='javascript' type='text/javascript'>alert('Уже есть такой клиент');</script>");

                Service.AddElement(new ClientBindingModel
                {
                    Id = client.Id,
                    ClientFIO = client.ClientFIO,
                    Login = client.Login,
                    Password = client.Password,
                    Mail = client.Mail
                });

                return RedirectToAction("Auth", "Clients");
            }

            return View(client);
        }

        public ActionResult Exit()
        {
            Globals.AuthClient = null;
            return RedirectToAction("Auth");
        }
    }
}