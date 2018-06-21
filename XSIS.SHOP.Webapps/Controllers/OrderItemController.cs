using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XSIS.SHOP.Webapps.Controllers
{
    public class OrderItemController : Controller
    {
        // GET: OrderItem
        public ActionResult Create()
        {
            return PartialView();
        }
    }
}