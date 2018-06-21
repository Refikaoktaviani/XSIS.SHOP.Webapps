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
    public class CustomerAPIController : ApiController
    {
        private CustomerRespository service = new CustomerRespository();

        //get fungsinya untuk tidak dikembalikan
        [HttpGet]
        public List<CustomerViewModel> Get()
        {
            var result = service.GetAllCustomer();
            return result;
        }

       

        [HttpGet]
        public CustomerViewModel Get(int Id)
        {
            var result = service.GetCustomerById(Id);
            return result;
        }

        [HttpGet]
        public List<CustomerViewModel> SearchByKey(string id)
        {
            string[] Parameters = id.Split('|');

            string param1 = Parameters[0];
            string param2 = Parameters[1];
            string param3 = Parameters[2];

            var result = service.SearchByKey(param1, param2, param3);
            return result;
        }

        [HttpGet]
        public bool SearchFullName (string id)
        {
            var a = service.SearchFullName(id);
            return a;
        }

        [HttpGet]
        public bool SearchEmail(string id)
        {
            var b = service.SearchEmail(id);
            return b;
        }


        [HttpPost]
        public int Post(CustomerViewModel customer)
        {
            try
            {
                service.AddNewCustomer(customer);
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        [HttpPut]
        public int Put(CustomerViewModel customer)
        {
            try
            {
                service.UpdateCustomer(customer);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        [HttpDelete]
        public int Delete(int Id)
        {
            try
            {
                service.DeleteCustomer(Id);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
