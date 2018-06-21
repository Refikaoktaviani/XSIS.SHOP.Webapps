using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsis.Shop.Models;
using XSIS.Shop.ViewModel;
using System.Data.Entity;

namespace XSIS.Shop.Repository
{
    public class ProductRepository
    {
        public List<ProductViewModel> GetAllProduct()
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {

                var list = db.Product.Include(p => p.Supplier);
                List<ProductViewModel> listVM = new List<ProductViewModel>();

                foreach (var item in list)
                {
                    ProductViewModel viewModel = new ProductViewModel();
                    viewModel.Id = item.Id;
                    viewModel.ProductName = item.ProductName;
                    viewModel.CompanyName = item.Supplier.CompanyName;
                    viewModel.SupplierId = item.SupplierId;
                    viewModel.UnitPrice = item.UnitPrice;
                    viewModel.Package = item.Package;
                    viewModel.IsDiscontinued = item.IsDiscontinued;

                   


                    listVM.Add(viewModel);
                }
                return listVM;
            }

           
        }

        public ProductViewModel GetProductById (int id)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Product product = db.Product.Find(id);

                ProductViewModel viewModel = new ProductViewModel();
                viewModel.Id = product.Id;
                viewModel.ProductName = product.ProductName;
                viewModel.CompanyName = product.Supplier.CompanyName;
                viewModel.SupplierId = product.SupplierId;
                viewModel.UnitPrice = product.UnitPrice;
                viewModel.Package = product.Package;
                viewModel.IsDiscontinued = product.IsDiscontinued;
                return viewModel;
            }
        }

        public void AddNewProduct (ProductViewModel product)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Product model = new Product();
                model.ProductName = product.ProductName;
                model.SupplierId = product.SupplierId;
                model.UnitPrice = product.UnitPrice;
                model.Package = product.Package;
                model.IsDiscontinued = product.IsDiscontinued;

                db.Product.Add(model);
                db.SaveChanges();
            }
        }

        public void UpdateProduct (ProductViewModel product)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Product model = new Product();
                model.Id = product.Id;
                model.ProductName = product.ProductName;
                model.SupplierId = product.SupplierId;
                model.UnitPrice = product.UnitPrice;
                model.Package = product.Package;
                model.IsDiscontinued = product.IsDiscontinued;


                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Product product = db.Product.Find(id);
                db.Product.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
