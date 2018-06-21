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
    public class ProductsAPIController : ApiController
    {
        private ProductRepository service = new ProductRepository();

        [HttpGet]
        public List<ProductViewModel> Get()
        {
            var result = service.GetAllProduct();
            return result;
        }

        [HttpGet]

        public ProductViewModel Get(int Id)
        {
            var result = service.GetProductById(Id);
            return result;
        }

        [HttpPost]
        public int Post(ProductViewModel product)
        {
            try
            {
                service.AddNewProduct(product);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        [HttpPut]
        public int Put(ProductViewModel product)
        {
            try
            {
                service.UpdateProduct (product);
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
                service.DeleteProduct(Id);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

    }
}
