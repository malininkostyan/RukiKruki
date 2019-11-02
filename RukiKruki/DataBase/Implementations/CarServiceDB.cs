using DAL.BindingModel;
using DAL.Interface;
using DAL.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase.Implementations
{
    public class CarServiceDB : ICarService
    {
        private readonly RukiKrukiDbContext context;

        public CarServiceDB(RukiKrukiDbContext context)
        {
            this.context = context;
        }

        public List<CarViewModel> GetClientCars(int clientId)
        {
            return GetList().Where(car => car.ClientId == clientId).ToList();
        }

        public void AddElement(CarBindingModel model)
        {
            var element = context.Cars.FirstOrDefault(rec => rec.VIN == model.VIN);

            if (element != null)
            {
                throw new Exception("Уже есть автомобиль с данным VIN-номером");
            }

            context.Cars.Add(new Car
            {
                VIN = model.VIN,
                Brand = model.Brand,
                ClientId = model.ClientId,
                Mileage = model.Mileage,
                Model = model.Model
            });
            context.SaveChanges();
        }

        public void DeleteElement(int id)
        {
            var element = context.Cars.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Cars.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public CarViewModel GetElement(int id)
        {
            var element = context.Cars.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CarViewModel
                {
                    Id = element.Id,
                    VIN = element.VIN,
                    Brand = element.Brand,
                    Mileage = element.Mileage,
                    Model = element.Model,
                    ClientId = element.ClientId
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<CarViewModel> GetList()
        {
            return context.Cars.Select(rec => new CarViewModel
            {
                Id = rec.Id,
                VIN = rec.VIN,
                Brand = rec.Brand,
                Mileage = rec.Mileage,
                Model = rec.Model,
                ClientId = rec.ClientId
            }).ToList();
        }

        public void UpdateElement(CarBindingModel model)
        {
            var element = context.Cars.FirstOrDefault(rec => rec.VIN == model.VIN && rec.Id != model.Id);

            if (element != null)
            {
                throw new Exception("Уже есть автомобиль с таким VIN-номером");
            }

            element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            element.VIN = model.VIN;
            element.Model = model.Model;
            element.Brand = model.Brand;
            element.Mileage = model.Mileage;
            context.SaveChanges();
        }
    }
}
