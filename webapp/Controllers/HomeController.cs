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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
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

            datalayer.interfaces.ITitleRepository titleRepository = new datalayer.repositories.TitleRepository();
            List<datalayer.models.Title> allTitles = titleRepository.GetAll();

            var titles = new List<Title>();
            titles.Add(new Title{ Id = 0, Name = "Please Select..." });
            foreach (var title in allTitles)
            {
                titles.Add(new Title{ Id = title.Id, Name = title.Name });
            }

            viewModel.SearchDetails = new AddCustomerViewModel{ Customer = new Customer(), Titles = titles };

            return View("Index", viewModel);
        }

        public IActionResult AddCustomer()
        {
            datalayer.interfaces.ITitleRepository titleRepository = new datalayer.repositories.TitleRepository();
            List<datalayer.models.Title> allTitles = titleRepository.GetAll();

            var titles = new List<Title>();
            foreach (var title in allTitles)
            {
                titles.Add(new Title{ Id = title.Id, Name = title.Name });
            }

            return View(new AddCustomerViewModel{ Customer = new Customer(), Titles = titles });
        }

        public IActionResult SaveCustomer(AddCustomerViewModel a)
        {
            var customer = new datalayer.models.Customer
            {
                Title = new datalayer.models.Title { Id = System.Convert.ToInt32(a.CustomerTitleId) },
                FirstName = a.Customer.FirstName,
                LastName = a.Customer.LastName,
                AddressLine1 = a.Customer.AddressLine1,
                AddressPostcode = a.Customer.AddressPostcode
            };

            datalayer.interfaces.ICustomerRepository customersRepository = new datalayer.repositories.CustomerRepository();
            customersRepository.Add(customer);

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
            datalayer.interfaces.ITitleRepository titleRepository = new datalayer.repositories.TitleRepository();
            List<datalayer.models.Title> allTitles = titleRepository.GetAll();

            var titles = new List<Title>();
            foreach (var title in allTitles)
            {
                titles.Add(new Title{ Id = title.Id, Name = title.Name });
            }

            datalayer.interfaces.ICustomerRepository customersRepository = new datalayer.repositories.CustomerRepository();
            var customer = customersRepository.Get(customerId);

            return View(new EditCustomerViewModel { 
                Customer = new Customer {
                    Id = customer.Id,
                    Title = customer.Title.Name,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    AddressLine1 = customer.AddressLine1,
                    AddressPostcode = customer.AddressPostcode 
                }, 
                CustomerTitleId = customer.Title.Id,
                Titles = titles 
            });
        }

        public IActionResult SaveEditCustomer(EditCustomerViewModel a)
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
            customersRepository.Update(customer);

            return RedirectToAction("Index");
        }
    }
}
