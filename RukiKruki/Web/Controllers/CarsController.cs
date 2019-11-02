using DAL.BindingModel;
using DAL.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class CarsController : Controller
    {
        public ICarService Service = Globals.CarService;
        // GET: Car
        public ActionResult Index()
        {
            return View(Service.GetClientCars(Globals.AuthClient.Id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientId,VIN,Brand,Model,Mileage")] Car car)
        {
            if (ModelState.IsValid)
            {
                var carDB = Service.GetList().FirstOrDefault(rec => rec.VIN == car.VIN);

                if (carDB != null)
                    return Content("<script language='javascript' type='text/javascript'>alert('Уже есть этот автомобиль');</script>");

                Service.AddElement(new CarBindingModel
                {
                    Id = car.Id,
                    ClientId = Globals.AuthClient.Id,
                    VIN = car.VIN,
                    Brand = car.Brand,
                    Model = car.Model,
                    Mileage = car.Mileage
                });

                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Elements/Delete/5
        public ActionResult Delete(int id)
        {
            Service.DeleteElement(id);
            return RedirectToAction("Index");
        }

        // GET: Elements/Edit/5
        public ActionResult Edit(int id)
        {
            var viewModel = Service.GetElement(id);
            var bindingModel = new CarBindingModel
            {
                Id = id,
                ClientId = Globals.AuthClient.Id,
                VIN = viewModel.VIN,
                Brand = viewModel.Brand,
                Model = viewModel.Model,
                Mileage = viewModel.Mileage
            };
            return View(bindingModel);
        }


        [HttpPost]
        public ActionResult EditPost()
        {
            Service.UpdateElement(new CarBindingModel
            {
                Id = int.Parse(Request["Id"]),
                ClientId = Globals.AuthClient.Id,
                VIN = Request["VIN"],
                Brand = Request["Brand"],
                Model = Request["Model"],
                Mileage = int.Parse(Request["Mileage"])
            });
            return RedirectToAction("Index");
        }

    }
}