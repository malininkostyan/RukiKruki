using DAL.BindingModel;
using DAL.Interface;
using DAL.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Implementations
{
    public class DetailServiceDB : IDetailService
    {
        readonly RukiKrukiDbContext context;

        public DetailServiceDB(RukiKrukiDbContext context)
        {
            this.context = context;
        }

        public void AddElement(DetailBindingModel model)
        {
            var detail = context.Details.FirstOrDefault(record => record.DetailName == model.DetailName);

            if (detail != null)
            {
                throw new Exception("Уже есть деталь");
            }

            context.Details.Add(new Detail
            {
                DetailName = model.DetailName,
                Amount = model.Amount
            });

            context.SaveChanges();
        }

        public void DeleteElement(int id)
        {
            var detail = context.Details.FirstOrDefault(
                record => record.Id == id);

            if (detail != null)
            {
                context.Details.Remove(detail);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Деталь не найдена");
            }
        }

        public DetailViewModel GetElement(int id)
        {
            var detail = context.Details.FirstOrDefault(record => record.Id == id);

            if (detail != null)
            {
                return new DetailViewModel
                {
                    Id = detail.Id,
                    DetailName = detail.DetailName,
                    Amount = detail.Amount
                };

            }
            throw new Exception("Деталь не найдена");
        }

        public List<DetailViewModel> GetList()
        {
            return context.Details.Select(record => new DetailViewModel
            {
                Id = record.Id,
                DetailName = record.DetailName,
                Amount = record.Amount
            }).ToList();
        }

        public void UpdateElement(DetailBindingModel model)
        {
            var detail = context.Details.FirstOrDefault(record => record.DetailName == model.DetailName && record.Id != model.Id);

            if (detail != null)
            {
                throw new Exception("Уже есть деталь");
            }

            detail = context.Details.FirstOrDefault(rec => rec.Id == model.Id);

            if (detail == null)
            {
                throw new Exception("Деталь не найдена");
            }

            detail.DetailName = model.DetailName;
            detail.Amount = model.Amount;

            context.SaveChanges();
        }
    }
}

