using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Xsis.Shop.Models;
using XSIS.Shop.Repository;
using XSIS.Shop.ViewModel;
using System.Web.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;


namespace XSIS.SHOP.Webapps.Controllers
{
    public class OrdersController : Controller
    {
        private SHOPDBEntities db = new SHOPDBEntities();
        private OrderRepository service = new OrderRepository();
        private string ApiURL = WebConfigurationManager.AppSettings["XSIS.SHOP.WebAPI"];

        // GET: Orders
        [HttpGet]
        public ActionResult Index(string OrderNumber,string OrderDate, string CustomerId)
        {
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName");
           
            

            List<OrderViewModel> list = null;
            var result = list;

            if (string.IsNullOrWhiteSpace(OrderNumber) && string.IsNullOrWhiteSpace(OrderDate) && string.IsNullOrWhiteSpace(CustomerId))
            {
                result = service.GetAllOrder();


            }
            else if (!string.IsNullOrEmpty(OrderNumber) || !string.IsNullOrEmpty(OrderDate) || !string.IsNullOrEmpty(CustomerId))
            {
                if (string.IsNullOrEmpty(OrderNumber) || string.IsNullOrWhiteSpace(OrderNumber))
                {
                    OrderNumber = "";
                }
                if (string.IsNullOrEmpty(OrderDate) || string.IsNullOrWhiteSpace(OrderDate))
                {
                    OrderDate = "";
                }
                if (string.IsNullOrEmpty(CustomerId) || string.IsNullOrWhiteSpace(CustomerId))
                {
                    CustomerId = "";
                }
                result = service.SearchByKey(OrderNumber,  OrderDate,CustomerId);

            }
            else
            {
                result = service.GetAllOrder();
            }



            return View(result.ToList());
        
    }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            //API Akses http://localhost:51082/api/Orders/1 (1 ini id)
            string ApiEndPoint = ApiURL + "api/OrderAPI/Get/" + idx;
            //OrderViewModel orderVM = service.GetOrderById(idx);


            //http client untuk mengakses url
            HttpClient client = new HttpClient();
            //http response untuk melihat hasil respon dari api akses
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

            //menampilkan resultnya dari http response
            string result = response.Content.ReadAsStringAsync().Result.ToString();

            OrderViewModel orderVM = JsonConvert.DeserializeObject<OrderViewModel>(result);
            if (orderVM == null)
            {
                return HttpNotFound();
            }
            return View(orderVM);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDate,OrderNumber,CustomerId,TotalAmount")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate,OrderNumber,CustomerId,TotalAmount")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
