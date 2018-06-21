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
    public class SupplierRepository
    {
        public List<SupplierViewModel> GetAllSupplier()
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {

                var list = db.Supplier.ToList();
                
                List<SupplierViewModel> listVM = new List<SupplierViewModel>();

                foreach (var item in list)
                {
                    SupplierViewModel viewModel = new SupplierViewModel();
                    viewModel.Id = item.Id;
                    viewModel.CompanyName = item.CompanyName;
                    viewModel.ContactName = item.ContactName;
                    viewModel.ContactTitle = item.ContactTitle;
                    viewModel.City = item.City;
                    viewModel.Country = item.Country;
                    viewModel.Phone = item.Phone;
                    viewModel.Fax = item.Fax;

                    listVM.Add(viewModel);
                }

                return listVM;
            }

        }

        public SupplierViewModel GetSupplierById (int id)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Supplier supplier = db.Supplier.Find(id);

                SupplierViewModel viewModel = new SupplierViewModel();
                viewModel.Id = supplier.Id;
                viewModel.CompanyName = supplier.CompanyName;
                viewModel.ContactName = supplier.ContactName;
                viewModel.ContactTitle = supplier.ContactTitle;
                viewModel.City = supplier.City;
                viewModel.Country = supplier.Country;
                viewModel.Phone = supplier.Phone;
                viewModel.Fax = supplier.Fax;

                List<ProductViewModel> ListProduct = new List<ProductViewModel>();
                var products = db.Product.Where(p => (p.SupplierId) == viewModel.Id).ToList();
                foreach (var listproduk in products)
                {
                    ProductViewModel p = new ProductViewModel();
                    p.Id = listproduk.Id;
                    p.ProductName = listproduk.ProductName;
                   
                    p.UnitPrice = listproduk.UnitPrice;
                    p.Package = listproduk.Package;
                    p.IsDiscontinued = listproduk.IsDiscontinued;

                    ListProduct.Add(p);
                }

                viewModel.listProduct = ListProduct;

                return viewModel;
            }
        }

        public void AddNewSupplier (SupplierViewModel supplier)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Supplier model = new Supplier();
                model.CompanyName = supplier.CompanyName;
                model.ContactName = supplier.ContactName;
                model.ContactTitle = supplier.ContactTitle;
                model.City = supplier.City;
                model.Country = supplier.Country;
                model.Phone = supplier.Phone;
                model.Fax = supplier.Fax;

                db.Supplier.Add(model);
                db.SaveChanges();
            }
        }

        public void UpdateSupplier (SupplierViewModel supplier)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Supplier model = new Supplier();
                model.Id = supplier.Id;
                model.CompanyName = supplier.CompanyName;
                model.ContactName = supplier.ContactName;
                model.ContactTitle = supplier.ContactTitle;
                model.City = supplier.City;
                model.Country = supplier.Country;
                model.Phone = supplier.Phone;
                model.Fax = supplier.Fax;


                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteSupplier (int id)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Supplier supplier = db.Supplier.Find(id);
                db.Supplier.Remove(supplier);
                db.SaveChanges();
            }
        }
    }
}
