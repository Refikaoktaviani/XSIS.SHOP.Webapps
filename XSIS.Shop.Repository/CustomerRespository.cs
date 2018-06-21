using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsis.Shop.Models;
using XSIS.Shop.ViewModel;

namespace XSIS.Shop.Repository
{
    public class CustomerRespository
    {
        //select* from customer (kalo di query) (diambil dari index di customerscontroller)
        public List<CustomerViewModel> GetAllCustomer()
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            { 
                    //var list = db.Customers.ToList();
                List<CustomerViewModel> listVM = new List<CustomerViewModel>();

                foreach (var item in db.Customer.ToList())
                {
                    CustomerViewModel viewModel = new CustomerViewModel();
                    viewModel.Id = item.Id;
                    viewModel.Id = item.Id;
                    viewModel.FirstName = item.FirstName;
                    viewModel.LastName = item.LastName;
                    viewModel.City = item.City;
                    viewModel.Country = item.Country;
                    viewModel.Phone = item.Phone;
                    viewModel.Email = item.Email;

                    listVM.Add(viewModel);
                }
                    return listVM;
            }
        }
        
        //select * from Customer where id =Id (diambil dari details di customerscontroller)
        public CustomerViewModel GetCustomerById( int id)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Customer customer = db.Customer.Find(id);

                CustomerViewModel viewModel = new CustomerViewModel();
                viewModel.Id = customer.Id;
                viewModel.FirstName = customer.FirstName;
                viewModel.LastName = customer.LastName;
                viewModel.City = customer.City;
                viewModel.Country = customer.Country;
                viewModel.Phone = customer.Phone;
                viewModel.Email = customer.Email;
                return viewModel;
            }
                
        }

        //select * from Customer where id =Id (diambil dari create di customerscontroller)
        public void AddNewCustomer(CustomerViewModel customer)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Customer model = new Customer();
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.City = customer.City;
                model.Country = customer.Country;
                model.Phone = customer.Phone;
                model.Email = customer.Email;

                db.Customer.Add(model);
                db.SaveChanges();
            
            }
        }

        //select * from Customer where id =Id (diambil dari edit di customerscontroller)
        public void UpdateCustomer (CustomerViewModel customer)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Customer model = new Customer();

                model.Id = customer.Id;
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.City = customer.City;
                model.Country = customer.Country;
                model.Phone = customer.Phone;
                model.Email = customer.Email;

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //select * from Customer where id =Id (diambil dari delete di customerscontroller)
        public void DeleteCustomer (int id)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                Customer customer = db.Customer.Find(id);
                db.Customer.Remove(customer);
                db.SaveChanges();
            }
        }
        public List<CustomerViewModel> SearchByKey(string FullName, string CityCountry, string Email)
        {
            
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                List<CustomerViewModel> listVM = new List<CustomerViewModel>();

                if (!String.IsNullOrEmpty(FullName) && String.IsNullOrEmpty(CityCountry) && string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customer.Where(m=> (m.FirstName+" "+m.LastName).ToLower().Contains(FullName.ToLower())))
                    {
                       
                            CustomerViewModel viewModel = new CustomerViewModel();
                            viewModel.Id = item.Id;
                            viewModel.Id = item.Id;
                            viewModel.FirstName = item.FirstName;
                            viewModel.LastName = item.LastName;
                            viewModel.City = item.City;
                            viewModel.Country = item.Country;
                            viewModel.Phone = item.Phone;
                            viewModel.Email = item.Email;

                            listVM.Add(viewModel);
                       
                    }
                }
                else if (String.IsNullOrEmpty(FullName) && !String.IsNullOrEmpty(CityCountry) && string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customer.Where (c => c.City.ToLower().Contains(CityCountry) || c.Country.ToLower().Contains(CityCountry)))
                    {

                        CustomerViewModel viewModel = new CustomerViewModel();
                        viewModel.Id = item.Id;
                        viewModel.Id = item.Id;
                        viewModel.FirstName = item.FirstName;
                        viewModel.LastName = item.LastName;
                        viewModel.City = item.City;
                        viewModel.Country = item.Country;
                        viewModel.Phone = item.Phone;
                        viewModel.Email = item.Email;

                        listVM.Add(viewModel);

                    }
                }
                else if (!String.IsNullOrEmpty(FullName) && !String.IsNullOrEmpty(CityCountry) && string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customer.Where(m => (m.FirstName + " " + m.LastName).ToLower().Contains(FullName.ToLower()) && ( m.City.ToLower().Contains(CityCountry) || m.Country.ToLower().Contains(CityCountry))))
                    {

                        CustomerViewModel viewModel = new CustomerViewModel();
                        viewModel.Id = item.Id;
                        viewModel.Id = item.Id;
                        viewModel.FirstName = item.FirstName;
                        viewModel.LastName = item.LastName;
                        viewModel.City = item.City;
                        viewModel.Country = item.Country;
                        viewModel.Phone = item.Phone;
                        viewModel.Email = item.Email;

                        listVM.Add(viewModel);
                    }
                }
                else if (!String.IsNullOrEmpty(FullName) && !String.IsNullOrEmpty(CityCountry) && !string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customer.Where(m => (m.FirstName + " " + m.LastName).ToLower().Contains(FullName.ToLower()) && (m.City.ToLower().Contains(CityCountry) || m.Country.ToLower().Contains(CityCountry)) && m.Email.ToLower().Contains(Email)))
                    {
                        CustomerViewModel viewModel = new CustomerViewModel();
                        viewModel.Id = item.Id;
                        viewModel.Id = item.Id;
                        viewModel.FirstName = item.FirstName;
                        viewModel.LastName = item.LastName;
                        viewModel.City = item.City;
                        viewModel.Country = item.Country;
                        viewModel.Phone = item.Phone;
                        viewModel.Email = item.Email;

                        listVM.Add(viewModel);
                    }
                }
                else if (!String.IsNullOrEmpty(FullName) && String.IsNullOrEmpty(CityCountry) && !string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customer.Where(m => (m.FirstName + " " + m.LastName).ToLower().Contains(FullName.ToLower())  && m.Email.ToLower().Contains(Email)))
                    {
                        CustomerViewModel viewModel = new CustomerViewModel();
                        viewModel.Id = item.Id;
                        viewModel.Id = item.Id;
                        viewModel.FirstName = item.FirstName;
                        viewModel.LastName = item.LastName;
                        viewModel.City = item.City;
                        viewModel.Country = item.Country;
                        viewModel.Phone = item.Phone;
                        viewModel.Email = item.Email;

                        listVM.Add(viewModel);
                    }
                }
                else if (String.IsNullOrEmpty(FullName) && !String.IsNullOrEmpty(CityCountry) && !string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customer.Where(m =>  (m.City.ToLower().Contains(CityCountry) || m.Country.ToLower().Contains(CityCountry)) && m.Email.ToLower().Contains(Email)))
                    {
                        CustomerViewModel viewModel = new CustomerViewModel();
                        viewModel.Id = item.Id;
                        viewModel.Id = item.Id;
                        viewModel.FirstName = item.FirstName;
                        viewModel.LastName = item.LastName;
                        viewModel.City = item.City;
                        viewModel.Country = item.Country;
                        viewModel.Phone = item.Phone;
                        viewModel.Email = item.Email;

                        listVM.Add(viewModel);
                    }
                }
                else if (String.IsNullOrEmpty(FullName) && String.IsNullOrEmpty(CityCountry) && !string.IsNullOrEmpty(Email))
                {
                    foreach (var item in db.Customer.Where(m => (m.Email.ToLower().Contains(Email))))
                    {
                        CustomerViewModel viewModel = new CustomerViewModel();
                        viewModel.Id = item.Id;
                        viewModel.Id = item.Id;
                        viewModel.FirstName = item.FirstName;
                        viewModel.LastName = item.LastName;
                        viewModel.City = item.City;
                        viewModel.Country = item.Country;
                        viewModel.Phone = item.Phone;
                        viewModel.Email = item.Email;

                        listVM.Add(viewModel);
                    }
                }


                    return listVM;
            }
        }

        public bool SearchFullName (string fullname)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                var namaLengkap = db.Customer.Where(m => (m.FirstName + " " + m.LastName).ToLower().Equals(fullname.ToLower())).FirstOrDefault();
               
                if (namaLengkap == null )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        public bool SearchEmail(string Email)
        {
            using (SHOPDBEntities db = new SHOPDBEntities())
            {
                var customeremail = db.Customer.Where(m => (m.Email).ToLower().Equals(Email.ToLower())).SingleOrDefault();
                if (customeremail == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

    }
}
