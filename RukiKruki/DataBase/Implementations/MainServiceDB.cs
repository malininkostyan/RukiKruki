using DAL.BindingModel;
using DAL.Interface;
using DAL.ViewModel;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private readonly RukiKrukiDbContext context;

        public MainServiceDB(RukiKrukiDbContext context)
        {
            this.context = context;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var element = new Order
                    {
                        ClientId = model.ClientId,
                        DateCreate = DateTime.Now,
                        TotalSum = model.TotalSum,
                        OrderStatus = OrderStatus.Принят
                    };
                    context.Orders.Add(element);
                    context.SaveChanges();

                    var groupTOs = model.OrderTOs
                        .GroupBy(rec => rec.TOId)
                        .Select(rec => new { TOId = rec.Key, Amount = rec.Sum(r => r.Amount) });

                    foreach (var groupTO in groupTOs)
                    {
                        context.OrderTOs.Add(new OrderTO
                        {
                            OrderId = element.Id,
                            TOId = groupTO.TOId,
                            Amount = groupTO.Amount
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

        public void FinishOrder(OrderBindingModel model)
        {
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            if (element.OrderStatus != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }

            element.DateImplement = DateTime.Now;
            element.OrderStatus = OrderStatus.Готов;
            context.SaveChanges();
        }

        public List<OrderViewModel> GetClientOrders(int clientId)
        {
            return GetList().Where(order => order.ClientId == clientId).ToList();
        }

        public List<OrderViewModel> GetList()
        {
            var result = context.Orders.Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                DateCreate =
                    SqlFunctions.DateName("dd", rec.DateCreate) + " " + SqlFunctions.DateName("mm", rec.DateCreate) +
                    " " + SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement =
                    rec.DateImplement == null
                        ? ""
                        : SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                          SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                          SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                StatusOrder = rec.OrderStatus.ToString(),
                TotalSum = rec.TotalSum,
                ClientFIO = rec.Client.ClientFIO,
                OrderTOs = context.OrderTOs.Where(recPC => recPC.OrderId == rec.Id)
                    .Select(recPC => new OrderTOViewModel
                    {
                        Id = recPC.Id,
                        TOId = recPC.TOId,
                        OrderId = recPC.OrderId,
                        TOName = recPC.TO.TOName,
                        Amount = recPC.Amount
                    })
                    .ToList()
            })
                .ToList();

            return result;
        }

        public OrderViewModel GetElement(int id)
        {
            var element = context.Orders.FirstOrDefault(rec => rec.Id == id);

            if (element != null)
            {
                return new OrderViewModel
                {
                    Id = element.Id,
                    ClientId = element.ClientId,
                    ClientFIO = context.Clients.FirstOrDefault(client => client.Id == element.ClientId)?.ClientFIO,
                    TotalSum = element.TotalSum,
                    StatusOrder = element.OrderStatus.ToString(),
                    DateCreate = element.DateCreate.ToString(CultureInfo.InvariantCulture),
                    DateImplement = element.DateImplement.ToString(),
                    OrderTOs = context.OrderTOs.Where(recOC => recOC.OrderId == element.Id)
                        .Select(recOC => new OrderTOViewModel
                        {
                            Id = recOC.Id,
                            OrderId = recOC.OrderId,
                            TOId = recOC.TOId,
                            TOName = recOC.TO.TOName,
                            Amount = recOC.Amount
                        })
                        .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void PayOrder(OrderBindingModel model)
        {
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.OrderStatus != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.OrderStatus = OrderStatus.Оплачен;
            context.SaveChanges();
        }

        public void ReserveOrder(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var element = new Order
                    {
                        ClientId = model.ClientId,
                        DateCreate = DateTime.Now,
                        TotalSum = model.TotalSum,
                        OrderStatus = OrderStatus.Зарезервирован
                    };

                    context.Orders.Add(element);
                    context.SaveChanges();

                    var groupTOs = model.OrderTOs
                        .GroupBy(rec => rec.TOId)
                        .Select(rec => new { TOId = rec.Key, Amount = rec.Sum(r => r.Amount) });

                    foreach (var groupTO in groupTOs)
                    {
                        var orderTO = new OrderTO
                        {
                            OrderId = element.Id,
                            TOId = groupTO.TOId,
                            Amount = groupTO.Amount
                        };

                        context.OrderTOs.Add(orderTO);

                        var _TODetails = context.TO_Details.Where(rec => rec.TOId == orderTO.TOId);

                        if (_TODetails.All(rec =>
                            rec.Amount <= context.Details.FirstOrDefault(r => r.Id == rec.DetailId).Amount))
                        {
                            foreach (var _TODetail in _TODetails)
                            {
                                var detail = context.Details.FirstOrDefault(r => r.Id == _TODetail.DetailId);

                                detail.Amount -= _TODetail.Amount;
                                detail.Reserve += _TODetail.Amount;
                            }
                        }
                        else
                        {
                            throw new Exception("Недостаточно деталей для резервации");
                        }

                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

        }

        public void TakeOrderInWork(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }

                    if (element.OrderStatus == OrderStatus.Принят || element.OrderStatus == OrderStatus.Зарезервирован)
                    {
                        var orderTOs = context.OrderTOs.Where(rec => rec.OrderId == element.Id);

                        if (element.OrderStatus == OrderStatus.Принят)
                        {
                            foreach (var orderTO in orderTOs)
                            {
                                var _TODetails = context.TO_Details.Where(rec => rec.TOId == orderTO.TOId);

                                if (_TODetails.Any(rec => rec.Amount > context.Details
                                                    .FirstOrDefault(det => det.Id == rec.DetailId).Amount))
                                {
                                    throw new Exception("Недостаточно деталей");
                                }
                            }

                            foreach (var orderTO in orderTOs)
                            {
                                var _TODetails = context.TO_Details.Where(rec => rec.TOId == orderTO.TOId);

                                foreach (var _TODetail in _TODetails)
                                {
                                    var detail = context.Details.FirstOrDefault(rec => rec.Id == _TODetail.DetailId);

                                    detail.Amount -= _TODetail.Amount;
                                }
                            }
                        }

                        if (element.OrderStatus == OrderStatus.Зарезервирован)
                        {
                            foreach (var orderTO in orderTOs)
                            {
                                var _TODetails = context.TO_Details.Where(rec => rec.TOId == orderTO.TOId);

                                foreach (var _TODetail in _TODetails)
                                {
                                    var detail = context.Details.FirstOrDefault(rec => rec.Id == _TODetail.DetailId);

                                    detail.Reserve -= _TODetail.Amount;
                                }
                            }
                        }

                        element.OrderStatus = OrderStatus.Выполняется;
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    else
                    {
                        throw new Exception("Заказ не в статусе \"Принят\" или \"Зарезервирован\"");
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void SaveDataBaseClient()
        {
            SaveEntity(context.Orders.ToList());
            SaveEntity(context.Clients.ToList());
            SaveEntity(context.OrderTOs.ToList());
            SaveEntity(context.Cars.ToList());
        }

        public void SaveDataBaseAdmin()
        {
            SaveEntity(context.TOs.ToList());
            SaveEntity(context.Details.ToList());
            SaveEntity(context.TO_Details.ToList());
            SaveEntity(context.RequestDetails.ToList());
            SaveEntity(context.Requests.ToList());
        }

        private static void SaveEntity(IEnumerable entity)
        {
            var jsonFormatter = new DataContractJsonSerializer(entity.GetType());

            using (var fs = new FileStream($"D:\\backup/{GetNameEntity(entity)}.json",
                FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, entity);
            }
        }

        private static string GetNameEntity(IEnumerable entity)
        {
            return entity.AsQueryable().ElementType.ToString().Split('.')[1];
        }
    }
}