using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using datalayer;
using webapp.Models;

namespace webapp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            datalayer.interfaces.ICustomerRepository customersRepository = new datalayer.repositories.CustomerRepository();
            var customers = customersRepository.GetAll();

            return IndexHelper(customers);
        }

        public IActionResult IndexHelper(List<datalayer.models.Customer> customers)
        {
            var viewModel = new IndexViewModel{ Customers = new List<Customer>() };

            foreach(var c in customers)
            {
                viewModel.Customers.Add(new Customer
                {
                    Id = c.Id,
                    Title = c.Title.Name,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    AddressLine1 = c.AddressLine1,
                    AddressPostcode = c.AddressPostcode
                });
            }

            viewModel.SearchDetails = new CustomerViewModel{ Customer = new Customer(), Titles = BuildTitleList() };

            return View("Index", viewModel);
        }

        public IActionResult AddCustomer()
        {
            return View(new CustomerViewModel{ Customer = new Customer(), Titles = BuildTitleList() });
        }

        public IActionResult SaveCustomer(CustomerViewModel a)
        {
            var customer = new datalayer.models.Customer
            {
                Id = a.Customer.Id,
                Title = new datalayer.models.Title { Id = System.Convert.ToInt32(a.CustomerTitleId) },
                FirstName = a.Customer.FirstName,
                LastName = a.Customer.LastName,
                AddressLine1 = a.Customer.AddressLine1,
                AddressPostcode = a.Customer.AddressPostcode
            };

            datalayer.interfaces.ICustomerRepository customersRepository = new datalayer.repositories.CustomerRepository();
            
            if (customer.Id == 0)
            {
                customersRepository.Add(customer);
            }
            else
            {
                customersRepository.Update(customer);
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCustomer(int customerId)
        {
            datalayer.interfaces.ICustomerRepository customersRepository = new datalayer.repositories.CustomerRepository();
            customersRepository.Delete(customerId);

            return RedirectToAction("Index");
        }

        public IActionResult SearchCustomers(IndexViewModel a)
        {
            var searchDetails = new datalayer.models.Customer
            {
                Title = new datalayer.models.Title { Id = System.Convert.ToInt32(a.SearchDetails.CustomerTitleId) },
                FirstName = a.SearchDetails.Customer.FirstName,
                LastName = a.SearchDetails.Customer.LastName,
                AddressLine1 = a.SearchDetails.Customer.AddressLine1,
                AddressPostcode = a.SearchDetails.Customer.AddressPostcode
            };

            datalayer.interfaces.ICustomerRepository customersRepository = new datalayer.repositories.CustomerRepository();
            var customers = customersRepository.Search(searchDetails);

            return IndexHelper(customers);
        }

        public IActionResult EditCustomer(int customerId)
        {
            datalayer.interfaces.ICustomerRepository customersRepository = new datalayer.repositories.CustomerRepository();
            var customer = customersRepository.Get(customerId);

            return View(new CustomerViewModel { 
                Customer = new Customer {
                    Id = customer.Id,
                    Title = customer.Title.Name,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    AddressLine1 = customer.AddressLine1,
                    AddressPostcode = customer.AddressPostcode 
                }, 
                CustomerTitleId = customer.Title.Id,
                Titles = BuildTitleList() 
            });
        }

        private List<Title> BuildTitleList()
        {
            datalayer.interfaces.ITitleRepository titleRepository = new datalayer.repositories.TitleRepository();
            List<datalayer.models.Title> allTitles = titleRepository.GetAll();

            var titles = new List<Title>();
            foreach (var title in allTitles)
            {
                titles.Add(new Title{ Id = title.Id, Name = title.Name });
            }

            return titles;
        }
    }
}
