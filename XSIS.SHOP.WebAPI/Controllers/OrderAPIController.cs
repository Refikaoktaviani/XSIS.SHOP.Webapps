using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XSIS.Shop.Repository;
using XSIS.Shop.ViewModel;

namespace XSIS.SHOP.WebAPI.Controllers
{
    public class OrderAPIController : ApiController
    {
        private OrderRepository service = new OrderRepository();

        [HttpGet]
        public OrderViewModel Get(int Id)
        {
            var result = service.GetDetailOrderById(Id);
            return result;
        }

        [HttpGet]
        public List<OrderViewModel> Get()
        {
            var result = service.GetAllOrder();
            return result;
        }
    

    }
}
