using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsis.Shop.Models;
using XSIS.Shop.ViewModel;

namespace XSIS.Shop.Repository
{
    public class OrderRepository
    {
        public OrderViewModel GetDetailOrderById(int Id)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                OrderViewModel model = new OrderViewModel();
                /* select a.OrderNumber, d.FirstName, c.ProductName
                  from(Order) a JOIN(OrderItem) b on a.Id = b.OrderId
                  JOIN Product c on b.Product = c.Id
                  JOIN Customer d on d.Id = a.CustomerId
                  where a.OrderNumber = '543278'*/

                model = (from a in db.Order
                         join d in db.Customer on a.CustomerId equals d.Id
                         where a.Id == Id
                         select new OrderViewModel
                         {
                             Id = a.Id,
                             OrderDate = a.OrderDate.ToString(),
                             OrderNumber = a.OrderNumber,
                             CustomerName = d.FirstName + " " + d.LastName,
                             CustomerId = d.Id,
                             TotalAmount = a.TotalAmount
                         }).Single();

                //mapping detail order(Order item)
                model.ListOrderItem = (from a in db.Order
                                       join b in db.OrderItem on a.Id equals b.OrderId
                                       join c in db.Product on b.ProductId equals c.Id
                                       where a.Id == Id
                                       select new OrderItemViewModel
                                       {

                                           Id = b.Id,
                                           OrderId = b.OrderId,
                                           OrderNumber = a.OrderNumber,
                                           ProductId = c.Id,
                                           ProductName = c.ProductName,
                                           UnitPrice = b.UnitPrice,
                                           Quantity = b.Quantity,
                                           TotalAmount = b.UnitPrice * b.Quantity

                                       }).ToList();
                return model;


            }
        }

        public List<OrderViewModel> GetAllOrder()
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                var list = db.Order.ToList();

                List<OrderViewModel> listVM = new List<OrderViewModel>();
               
                foreach (var item in db.Order.ToList())
                {
                    OrderViewModel viewModel = new OrderViewModel();
                    viewModel.Id = item.Id;
                    viewModel.OrderDate = item.OrderDate.ToString();
                    viewModel.OrderNumber = item.OrderNumber;
                    viewModel.CustomerName = item.Customer.FirstName + " " + item.Customer.LastName;
                    viewModel.CustomerId = item.CustomerId;
                    viewModel.TotalAmount = item.TotalAmount;


                    listVM.Add(viewModel);

                }
                return listVM;

            }
        }

        public List<OrderViewModel> SearchByKey(string OrderNumber, string OrderDate, string CustomerId)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
               
                List<OrderViewModel> listVM = new List<OrderViewModel>();
                var model = new List<OrderViewModel>();
                if (!String.IsNullOrEmpty(OrderNumber) && String.IsNullOrEmpty(OrderDate) && string.IsNullOrEmpty(CustomerId))
                {
                    model = (from a in db.Order
                             join d in db.Customer on a.CustomerId equals d.Id
                             where a.OrderNumber == OrderNumber
                             select new OrderViewModel
                             {
                                 Id = a.Id,
                                 OrderDate = a.OrderDate.ToString(),
                                 OrderNumber = a.OrderNumber,
                                 CustomerName = d.FirstName + " " + d.LastName,
                                 CustomerId = d.Id,
                                 TotalAmount = a.TotalAmount
                             }).ToList();

                   
                }

                else if (String.IsNullOrEmpty(OrderNumber) && !String.IsNullOrEmpty(OrderDate) && string.IsNullOrEmpty(CustomerId))
                {
                    string[] parameter = OrderDate.Split('/');

                    DateTime date = DateTime.Parse((parameter[1]+'/'+parameter[0]+'/'+parameter[2]));
                    model = (from a in db.Order
                             join d in db.Customer on a.CustomerId equals d.Id
                             where a.OrderDate == date
                             select new OrderViewModel
                             {
                                 Id = a.Id,
                                 OrderDate = a.OrderDate.ToString(),
                                 OrderNumber = a.OrderNumber,
                                 CustomerName = d.FirstName + " " + d.LastName,
                                 CustomerId = d.Id,
                                 TotalAmount = a.TotalAmount
                             }).ToList();
                }

                else if (String.IsNullOrEmpty(OrderNumber) && String.IsNullOrEmpty(OrderDate) && !string.IsNullOrEmpty(CustomerId))
                {
                    int CustomerIdbaru = int.Parse(CustomerId);
                    model = (from a in db.Order
                             join d in db.Customer on a.CustomerId equals d.Id
                             where a.CustomerId == CustomerIdbaru
                             select new OrderViewModel
                             {
                                 Id = a.Id,
                                 OrderDate = a.OrderDate.ToString(),
                                 OrderNumber = a.OrderNumber,
                                 CustomerName = d.FirstName + " " + d.LastName,
                                 CustomerId = d.Id,
                                 TotalAmount = a.TotalAmount
                             }).ToList();
                }

                else if (!String.IsNullOrEmpty(OrderNumber) && !String.IsNullOrEmpty(OrderDate) && string.IsNullOrEmpty(CustomerId))
                {
                    string[] parameter = OrderDate.Split('/');

                    DateTime date = DateTime.Parse((parameter[1] + '/' + parameter[0] + '/' + parameter[2]));
                    model = (from a in db.Order
                             join d in db.Customer on a.CustomerId equals d.Id
                             where a.OrderNumber == OrderNumber && a.OrderDate == date
                             select new OrderViewModel
                             {
                                 Id = a.Id,
                                 OrderDate = a.OrderDate.ToString(),
                                 OrderNumber = a.OrderNumber,
                                 CustomerName = d.FirstName + " " + d.LastName,
                                 CustomerId = d.Id,
                                 TotalAmount = a.TotalAmount
                             }).ToList();

                }

                else if (!String.IsNullOrEmpty(OrderNumber) && String.IsNullOrEmpty(OrderDate) && !string.IsNullOrEmpty(CustomerId))
                {
                    int CustomerIdbaru = int.Parse(CustomerId);
                    model = (from a in db.Order
                             join d in db.Customer on a.CustomerId equals d.Id
                             where a.OrderNumber == OrderNumber && a.CustomerId == CustomerIdbaru
                             select new OrderViewModel
                             {
                                 Id = a.Id,
                                 OrderDate = a.OrderDate.ToString(),
                                 OrderNumber = a.OrderNumber,
                                 CustomerName = d.FirstName + " " + d.LastName,
                                 CustomerId = d.Id,
                                 TotalAmount = a.TotalAmount
                             }).ToList();

                }
                else if (String.IsNullOrEmpty(OrderNumber) && !String.IsNullOrEmpty(OrderDate) && !string.IsNullOrEmpty(CustomerId))
                {
                    int CustomerIdbaru = int.Parse(CustomerId);
                    string[] parameter = OrderDate.Split('/');

                    DateTime date = DateTime.Parse((parameter[1] + '/' + parameter[0] + '/' + parameter[2]));
                    model = (from a in db.Order
                             join d in db.Customer on a.CustomerId equals d.Id
                             where a.OrderDate == date && a.CustomerId == CustomerIdbaru
                             select new OrderViewModel
                             {
                                 Id = a.Id,
                                 OrderDate = a.OrderDate.ToString(),
                                 OrderNumber = a.OrderNumber,
                                 CustomerName = d.FirstName + " " + d.LastName,
                                 CustomerId = d.Id,
                                 TotalAmount = a.TotalAmount
                             }).ToList();
                }
                else if (!String.IsNullOrEmpty(OrderNumber) && !String.IsNullOrEmpty(OrderDate) && !string.IsNullOrEmpty(CustomerId))
                {
                    string[] parameter = OrderDate.Split('/');

                    DateTime date = DateTime.Parse((parameter[1] + '/' + parameter[0] + '/' + parameter[2]));
                    int CustomerIdbaru = int.Parse(CustomerId);
                  
                    model = (from a in db.Order
                             join d in db.Customer on a.CustomerId equals d.Id
                             where a.OrderNumber == OrderNumber &&a.OrderDate == date && a.CustomerId == CustomerIdbaru
                             select new OrderViewModel
                             {
                                 Id = a.Id,
                                 OrderDate = a.OrderDate.ToString(),
                                 OrderNumber = a.OrderNumber,
                                 CustomerName = d.FirstName + " " + d.LastName,
                                 CustomerId = d.Id,
                                 TotalAmount = a.TotalAmount
                             }).ToList();
                }

                return model;
            }


        }

    }
}
