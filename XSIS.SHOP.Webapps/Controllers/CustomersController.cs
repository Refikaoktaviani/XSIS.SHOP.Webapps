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
    public class CustomersController : Controller
    {
        private string ApiURL = WebConfigurationManager.AppSettings["XSIS.SHOP.WebAPI"];
        private CustomerRespository service = new CustomerRespository();

     
        [HttpGet]
        public ActionResult Index(string FullName, string CityCountry, string Email)
        {
            List<CustomerViewModel> list = null;
            var result = list;
            
            if (string.IsNullOrWhiteSpace(FullName) && string.IsNullOrWhiteSpace(CityCountry) && string.IsNullOrWhiteSpace(Email))
            {

                // Get All Customer API Akses http://localhost:2099/api/CustomerApi/ without parameter
                string ApiEndPoint = ApiURL + "api/CustomerAPI/";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

                string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
                result = JsonConvert.DeserializeObject<List<CustomerViewModel>>(ListResult);

            }
            else if (!string.IsNullOrEmpty(FullName) || !string.IsNullOrEmpty(CityCountry) || !string.IsNullOrEmpty(Email))
            {
                if (string.IsNullOrEmpty(FullName) || string.IsNullOrWhiteSpace(FullName))
                {
                    FullName = "";
                }
                if (string.IsNullOrEmpty(CityCountry) || string.IsNullOrWhiteSpace(CityCountry))
                {
                    CityCountry = "";
                }
                if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
                {
                    Email= "";
                }
                result = service.SearchByKey(FullName, CityCountry, Email);
                // Get All Customer API Akses http://localhost:2099/api/CustomerApi/FullName/CityCountry/Email with parameter
                string ApiEndPoint = ApiURL + "api/CustomerAPI/SearchByKey/" + (FullName + "|" + CityCountry + "|" + Email);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

                string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
                result = JsonConvert.DeserializeObject<List<CustomerViewModel>>(ListResult);

            }
            else
            {
                // Get All Customer API Akses http://localhost:2099/api/CustomerApi/ without parameter
                string ApiEndPoint = ApiURL + "api/CustomerAPI/";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

                string ListResult = response.Content.ReadAsStringAsync().Result.ToString();
                result = JsonConvert.DeserializeObject<List<CustomerViewModel>>(ListResult);
            }
            
           

            return View(result.ToList());
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;

            //API Akses http://localhost:51082/api/Customers/1 (1 ini id)
            string ApiEndPoint = ApiURL + "api/CustomerAPI/Get/" + idx;
            //CustomerViewModel custVM = service.GetCustomerById(idx);


            //http client untuk mengakses url
            HttpClient client = new HttpClient();
            //http response untuk melihat hasil respon dari api akses
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

            //menampilkan resultnya dari http response
            string result = response.Content.ReadAsStringAsync().Result.ToString();

            CustomerViewModel custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);
           
            if (custVM == null)
            {
                return HttpNotFound();
            }
            return View(custVM);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( CustomerViewModel customer)
        {
            bool Email = false;

            

            if (ModelState.IsValid)
            {
                bool fullname = service.SearchFullName(customer.FirstName + " " + customer.LastName);

                if ((!String.IsNullOrWhiteSpace(customer.Email) || !String.IsNullOrEmpty(customer.Email)))
                {
                    Email = service.SearchEmail(customer.Email);
                    if (Email == true)
                    {
                        ModelState.AddModelError("", "Email telah terpakai ");

                    }
                }
                if (fullname)
                {
                    ModelState.AddModelError("", "Nama depan dan nama belakang telah terpakai ");
                    
                }
                
                else
                {
                    //API Akses http://localhost:51082/api/Customers/1 (1 ini id)

                    string Json = JsonConvert.SerializeObject(customer);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(Json);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");



                    string ApiEndPoint = ApiURL + "api/CustomerAPI/" ;
                    //CustomerViewModel custVM = service.GetCustomerById(idx);


                    //http client untuk mengakses url
                    HttpClient client = new HttpClient();
                    //http response untuk melihat hasil respon dari api akses
                    HttpResponseMessage response = client.PostAsync(ApiEndPoint, byteContent).Result;

                    //menampilkan resultnya dari http response
                    string result = response.Content.ReadAsStringAsync().Result.ToString();
                    int success = int.Parse(result);

            

                    if (success==1)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(customer);
                    }
           
                   
                }
               
            }

           
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;

            //API Akses http://localhost:51082/api/Customers/1 (1 ini id)
            string ApiEndPoint = ApiURL + "api/CustomerAPI/" + idx;
            //CustomerViewModel custVM = service.GetCustomerById(idx);


            //http client untuk mengakses url
            HttpClient client = new HttpClient();
            //http response untuk melihat hasil respon dari api akses
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

            //menampilkan resultnya dari http response
            string result = response.Content.ReadAsStringAsync().Result.ToString();

            CustomerViewModel custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);

            if (custVM == null)
            {
                return HttpNotFound();
            }
         
          
            return View(custVM);

        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                //// instansiasi untuk customer
                //Customer model = new Customer();

                //model.Id = customer.Id;
                //model.FirstName = customer.FirstName;
                //model.LastName = customer.LastName;
                //model.City = customer.City;
                //model.Country = customer.Country;
                //model.Phone = customer.Phone;
                //model.Email = customer.Email;

                //db.Entry(customer).State = EntityState.Modified;
                //db.SaveChanges();
                //service.UpdateCustomer(customer);
                //return RedirectToAction("Index");
                //API Akses http://localhost:51082/api/Customers/1 (1 ini id)

                string Json = JsonConvert.SerializeObject(customer);
                var buffer = System.Text.Encoding.UTF8.GetBytes(Json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");



                string ApiEndPoint = ApiURL + "api/CustomerAPI/";
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
                    return View(customer);
                }
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idx = id.HasValue ? id.Value : 0;
            //Delete API Akses http://localhost:51082/api/Customers/1 (1 ini id)
            string ApiEndPoint = ApiURL + "api/CustomerAPI/Get/" + idx;
            //CustomerViewModel custVM = service.GetCustomerById(idx);


            //http client untuk mengakses url
            HttpClient client = new HttpClient();
            //http response untuk melihat hasil respon dari api akses
            HttpResponseMessage response = client.GetAsync(ApiEndPoint).Result;

            //menampilkan resultnya dari http response
            string result = response.Content.ReadAsStringAsync().Result.ToString();

            CustomerViewModel custVM = JsonConvert.DeserializeObject<CustomerViewModel>(result);
           
          

            if (custVM == null)
            {
                return HttpNotFound();
            }
        
            return View(custVM);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

  



            string ApiEndPoint = ApiURL + "api/CustomerAPI/" + id;
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
