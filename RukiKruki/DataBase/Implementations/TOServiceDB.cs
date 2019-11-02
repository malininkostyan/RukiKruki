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
    public class TOServiceDB : ITOService
    {
        readonly RukiKrukiDbContext context;

        public TOServiceDB(RukiKrukiDbContext context)
        {
            this.context = context;
        }

        public void AddElement(TOBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var _TO = context.TOs.FirstOrDefault(record => record.TOName == model.TOName);

                    if (_TO != null)
                    {
                        throw new Exception("Такое ТО уже существует.");
                    }
                    else
                    {
                        _TO = new TO
                        {
                            TOName = model.TOName,
                            Price = model.Price
                        };
                    }

                    context.TOs.Add(_TO);
                    context.SaveChanges();

                    var duplicates = model.TODetails
                        .GroupBy(record => record.DetailId)
                        .Select(record => new
                        {
                            detailId = record.Key,
                            amount = record.Sum(rec => rec.Amount)
                        });

                    foreach (var duplicate in duplicates)
                    {
                        context.TO_Details.Add(new TO_Detail
                        {
                            TOId = _TO.Id,
                            DetailId = duplicate.detailId,
                            Amount = duplicate.amount
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var _TO = context.TOs.FirstOrDefault(record => record.Id == id);

                    if (_TO != null)
                    {
                        context.TO_Details.RemoveRange(
                            context.TO_Details.Where(
                                record => record.TOId == id));

                        context.TOs.Remove(_TO);

                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("ТО не найдено");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public TOViewModel GetElement(int id)
        {
            var _TO = context.TOs.FirstOrDefault(record => record.Id == id);

            if (_TO != null)
            {
                return new TOViewModel
                {
                    Id = _TO.Id,

                    TOName = _TO.TOName,

                    Price = _TO.Price,

                    TODetails = context.TO_Details
                        .Where(recordCD => recordCD.TOId == _TO.Id)
                        .Select(recCD => new TO_DetailViewModel
                        {
                            Id = recCD.Id,
                            TOId = recCD.TOId,
                            DetailId = recCD.DetailId,
                            DetailName = recCD.Detail.DetailName,
                            Amount = recCD.Amount
                        }).ToList()
                };
            }
            throw new Exception("ТО не найдено");
        }

        public List<TOViewModel> GetFilteredList()
        {
            var result = new List<TOViewModel>();

            var _TOs = context.TOs.Select(rec => new TOViewModel
            {
                Id = rec.Id,
                TOName = rec.TOName,
                Price = rec.Price,
                TODetails = context.TO_Details
                .Where(recCD => recCD.TOId == rec.Id)
                .Select(recCD => new TO_DetailViewModel
                {
                    Id = recCD.Id,
                    TOId = recCD.TOId,
                    DetailId = recCD.DetailId,
                    DetailName = recCD.Detail.DetailName,
                    Amount = recCD.Amount
                }).ToList()
            }).ToList();

            foreach (var _TO in _TOs)
            {
                var _TODetails = context.TO_Details.Where(rec => rec.TOId == _TO.Id);

                if (_TODetails.Any(rec => rec.Amount > context.Details.FirstOrDefault(det => det.Id == rec.DetailId).Amount)) continue;

                result.Add(_TO);
            }

            return result;
        }

        public List<TOViewModel> GetList()
        {
            List<TOViewModel> result = context.TOs.Select(record => new TOViewModel
            {
                Id = record.Id,
                TOName = record.TOName,
                Price = record.Price,
                TODetails = context.TO_Details.Where(recordTODetails => recordTODetails.TOId == record.Id).Select(recordTODetails => new TO_DetailViewModel
                {
                    Id = recordTODetails.Id,
                    TOId = recordTODetails.TOId,
                    DetailId = recordTODetails.DetailId,
                    DetailName = recordTODetails.Detail.DetailName,
                    Amount = recordTODetails.Amount
                }).ToList()

            }).ToList();

            return result;
        }

        public void UpdateElement(TOBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var _TO = context.TOs
                        .FirstOrDefault(record => record.TOName == model.TOName && record.Id != model.Id);

                    if (_TO != null)
                    {
                        throw new Exception("Уже есть ТО с таким названием");
                    }

                    _TO = context.TOs
                        .FirstOrDefault(record => record.Id == model.Id);

                    if (_TO == null)
                    {
                        throw new Exception("ТО не найдено");
                    }

                    _TO.TOName = model.TOName;
                    _TO.Price = model.Price;
                    context.SaveChanges();

                    var IDs = model.TODetails.Select(
                        record => record.DetailId)
                        .Distinct();

                    var updateDetails = context.TO_Details.Where(
                        record => record.TOId == model.Id && IDs.Contains(record.DetailId));

                    foreach (var updateDetail in updateDetails)
                    {
                        updateDetail.Amount = model.TODetails.FirstOrDefault(record => record.Id == updateDetail.Id).Amount;
                    }

                    context.SaveChanges();

                    context.TO_Details
                        .RemoveRange(context.TO_Details
                            .Where(record => record.TOId == model.Id && !IDs.Contains(record.DetailId)));

                    context.SaveChanges();

                    var groupDetails = model.TODetails
                        .Where(record => record.Id == 0)
                        .GroupBy(record => record.DetailId)
                        .Select(record => new { detailId = record.Key, amount = record.Sum(r => r.Amount) });

                    foreach (var groupDetail in groupDetails)
                    {
                        var detail = context.TO_Details
                            .FirstOrDefault(record => record.TOId == model.Id && record.DetailId == groupDetail.detailId);

                        if (detail != null)
                        {
                            detail.Amount += groupDetail.amount;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.TO_Details.Add(new TO_Detail
                            {
                                TOId = model.Id,
                                DetailId = groupDetail.detailId,
                                Amount = groupDetail.amount
                            });

                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}

