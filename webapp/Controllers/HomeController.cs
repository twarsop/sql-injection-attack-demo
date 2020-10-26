using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataLayer;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DataLayer.Interfaces.ICustomerRepository customersRepository = new DataLayer.Repositories.CustomerRepository();
            var customers = customersRepository.GetAll();

            return IndexHelper(customers);
        }

        public IActionResult IndexHelper(List<DataLayer.Models.Customer> customers)
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
            var customer = new DataLayer.Models.Customer
            {
                Id = a.Customer.Id,
                Title = new DataLayer.Models.Title { Id = System.Convert.ToInt32(a.CustomerTitleId) },
                FirstName = a.Customer.FirstName,
                LastName = a.Customer.LastName,
                AddressLine1 = a.Customer.AddressLine1,
                AddressPostcode = a.Customer.AddressPostcode
            };

            DataLayer.Interfaces.ICustomerRepository customersRepository = new DataLayer.Repositories.CustomerRepository();
            
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
            DataLayer.Interfaces.ICustomerRepository customersRepository = new DataLayer.Repositories.CustomerRepository();
            customersRepository.Delete(customerId);

            return RedirectToAction("Index");
        }

        public IActionResult SearchCustomers(IndexViewModel a)
        {
            var searchDetails = new DataLayer.Models.Customer
            {
                Title = new DataLayer.Models.Title { Id = System.Convert.ToInt32(a.SearchDetails.CustomerTitleId) },
                FirstName = a.SearchDetails.Customer.FirstName,
                LastName = a.SearchDetails.Customer.LastName,
                AddressLine1 = a.SearchDetails.Customer.AddressLine1,
                AddressPostcode = a.SearchDetails.Customer.AddressPostcode
            };

            DataLayer.Interfaces.ICustomerRepository customersRepository = new DataLayer.Repositories.CustomerRepository();
            var customers = customersRepository.Search(searchDetails);

            return IndexHelper(customers);
        }

        public IActionResult EditCustomer(int customerId)
        {
            DataLayer.Interfaces.ICustomerRepository customersRepository = new DataLayer.Repositories.CustomerRepository();
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
            DataLayer.Interfaces.ITitleRepository titleRepository = new DataLayer.Repositories.TitleRepository();
            List<DataLayer.Models.Title> allTitles = titleRepository.GetAll();

            var titles = new List<Title>();
            titles.Add(new Title{ Id = 0, Name = "Please Select..." });
            foreach (var title in allTitles)
            {
                titles.Add(new Title{ Id = title.Id, Name = title.Name });
            }

            return titles;
        }
    }
}
