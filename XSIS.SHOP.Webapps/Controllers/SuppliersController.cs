using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XSIS.Shop.Repository;
using XSIS.Shop.ViewModel;
using System.Web.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;


namespace XSIS.SHOP.Webapps.Controllers
{
    public class SuppliersController : Controller

    {
        private string ApiURL = WebConfigurationManager.AppSettings["XSIS.SHOP.WebAPI"];
        private SupplierRepository service = new SupplierRepository();

        [HttpGet]
        public ActionResult Index()
        {
            var result = service.GetAllSupplier();
            return View(result);

        }


        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;

            //API Akses http://localhost:51082/api/Supplier/1 (1 ini id)
            string ApiEndPoint = ApiURL + "api/SupplierAPI/Get/" + idx;
            //CustomerViewModel custVM = service.GetCustomerById(idx);


            //http client untuk mengakses url
            HttpClient client = new HttpClient();
            //http response untuk melihat hasil respon dari api akses
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

            //menampilkan resultnya dari http response
            string result = response.Content.ReadAsStringAsync().Result.ToString();

            SupplierViewModel supplierVM = JsonConvert.DeserializeObject<SupplierViewModel>(result);


            if (supplierVM == null)
            {
                return HttpNotFound();
            }

            return View(supplierVM);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SupplierViewModel supplier)
        {
            if (ModelState.IsValid)
            {
                string Json = JsonConvert.SerializeObject(supplier);
                var buffer = System.Text.Encoding.UTF8.GetBytes(Json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");



                string ApiEndPoint = ApiURL + "api/SupplierAPI/";
                //CustomerViewModel custVM = service.GetCustomerById(idx);


                //http client untuk mengakses url
                HttpClient client = new HttpClient();
                //http response untuk melihat hasil respon dari api akses
                HttpResponseMessage response = client.PostAsync(ApiEndPoint, byteContent).Result;

                //menampilkan resultnya dari http response
                string result = response.Content.ReadAsStringAsync().Result.ToString();
                int success = int.Parse(result);



                if (success == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(supplier);
                }

                
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;


            //API Akses http://localhost:51082/api/Product/1 (1 ini id)
            string ApiEndPoint = ApiURL + "api/SupplierAPI/" + idx;
            //CustomerViewModel custVM = service.GetCustomerById(idx);


            //http client untuk mengakses url
            HttpClient client = new HttpClient();
            //http response untuk melihat hasil respon dari api akses
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

            //menampilkan resultnya dari http response
            string result = response.Content.ReadAsStringAsync().Result.ToString();

            SupplierViewModel SupplierVM = JsonConvert.DeserializeObject<SupplierViewModel>(result);

            if (SupplierVM == null)
            {
                return HttpNotFound();
            }


            return View(SupplierVM);

        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierViewModel supplier)
        {
            if (ModelState.IsValid)
            {
                string Json = JsonConvert.SerializeObject(supplier);
                var buffer = System.Text.Encoding.UTF8.GetBytes(Json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");



                string ApiEndPoint = ApiURL + "api/SupplierAPI/";
                //CustomerViewModel custVM = service.GetCustomerById(idx);


                //http client untuk mengakses url
                HttpClient client = new HttpClient();
                //http response untuk melihat hasil respon dari api akses
                HttpResponseMessage response = client.PutAsync(ApiEndPoint, byteContent).Result;

                //menampilkan resultnya dari http response
                string result = response.Content.ReadAsStringAsync().Result.ToString();
                int success = int.Parse(result);



                if (success == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(supplier);
                }
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            //Delete API Akses http://localhost:51082/api/Customers/1 (1 ini id)
            string ApiEndPoint = ApiURL + "api/SupplierAPI/Get/" + idx;
            //CustomerViewModel custVM = service.GetCustomerById(idx);


            //http client untuk mengakses url
            HttpClient client = new HttpClient();
            //http response untuk melihat hasil respon dari api akses
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

            //menampilkan resultnya dari http response
            string result = response.Content.ReadAsStringAsync().Result.ToString();

            SupplierViewModel supplierVM = JsonConvert.DeserializeObject<SupplierViewModel>(result);



            if (supplierVM == null)
            {
                return HttpNotFound();
            }

            return View(supplierVM);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            string ApiEndPoint = ApiURL + "api/SupplierAPI/" + id;
            //CustomerViewModel custVM = service.GetCustomerById(idx);


            //http client untuk mengakses url
            HttpClient client = new HttpClient();
            //http response untuk melihat hasil respon dari api akses
            HttpResponseMessage response = client.DeleteAsync(ApiEndPoint).Result;

            //menampilkan resultnya dari http response
            string result = response.Content.ReadAsStringAsync().Result.ToString();
            int success = int.Parse(result);



            if (success == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
           
        }

     
    }
}
