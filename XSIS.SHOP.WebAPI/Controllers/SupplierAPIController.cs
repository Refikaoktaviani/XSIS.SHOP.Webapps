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
    public class SupplierAPIController : ApiController
    {
        private SupplierRepository service = new SupplierRepository();

        [HttpGet]
        public List<SupplierViewModel> Get()
        {
            var result = service.GetAllSupplier();
            return result;
        }
        [HttpGet]

        public SupplierViewModel Get(int Id)
        {
            var result = service.GetSupplierById(Id);
            return result;
        }


        [HttpPost]
        public int Post(SupplierViewModel supplier)
        {
            try
            {
                service.AddNewSupplier(supplier);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        [HttpPut]
        public int Put(SupplierViewModel supplier)
        {
            try
            {
                service.UpdateSupplier(supplier);
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
                service.DeleteSupplier(Id);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
    }
