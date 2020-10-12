using System.Collections.Generic;
using System.IO;
using datalayer.interfaces;
    using datalayer.models;

namespace datalayer.repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ITitleRepository _titleRepository;
        private readonly string _customerFileLocation = @"C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\datalayer\datalayer\data\customers.csv";

        public CustomerRepository()
        {
            _titleRepository = new TitleRepository();
        }

        public Customer Get(int id)
        {
            // preload the list of titles and store in dictionary for later use
            List<Title> titles = _titleRepository.GetAll();
            var titleLookup = new Dictionary<int, Title>();
            foreach (Title title in titles)
            {
                titleLookup.Add(title.Id, title);
            }

            var customer = new Customer();

            using (StreamReader sr = new StreamReader(_customerFileLocation))
            {
                // read the header
                sr.ReadLine();

                while(sr.Peek() != -1)
                {
                    string[] splitLine = sr.ReadLine().Split(new[] { ","}, System.StringSplitOptions.RemoveEmptyEntries);
                    if (System.Convert.ToInt32(splitLine[0]) == id)
                    {
                        customer = new Customer
                        {
                            Id = System.Convert.ToInt32(splitLine[0]),
                            Title = titleLookup[System.Convert.ToInt32(splitLine[1])],
                            FirstName = splitLine[2],
                            LastName = splitLine[3],
                            AddressLine1 = splitLine[4],
                            AddressPostcode = splitLine[5]
                        };

                        break;
                    }                    
                }
            }

            return customer;
        }

        public List<Customer> GetAll()
        {
            // preload the list of titles and store in dictionary for later use
            List<Title> titles = _titleRepository.GetAll();
            var titleLookup = new Dictionary<int, Title>();
            foreach (Title title in titles)
            {
                titleLookup.Add(title.Id, title);
            }

            List<Customer> customers = new List<Customer>();

            using (StreamReader sr = new StreamReader(_customerFileLocation))
            {
                // read the header
                sr.ReadLine();

                while(sr.Peek() != -1)
                {
                    string[] splitLine = sr.ReadLine().Split(new[] { ","}, System.StringSplitOptions.RemoveEmptyEntries);
                    customers.Add(new Customer
                    {
                        Id = System.Convert.ToInt32(splitLine[0]),
                        Title = titleLookup[System.Convert.ToInt32(splitLine[1])],
                        FirstName = splitLine[2],
                        LastName = splitLine[3],
                        AddressLine1 = splitLine[4],
                        AddressPostcode = splitLine[5]
                    });
                }
            }

            return customers;
        }

        public List<Customer> Search(Customer c)
        {
            List<Customer> allCustomers = this.GetAll();

            var searchResults = new List<Customer>();

            foreach (Customer customer in allCustomers)
            {
                if ( 
                    (c.Title.Id == 0 || c.Title.Id == customer.Title.Id)
                    && (string.IsNullOrEmpty(c.FirstName) || c.FirstName == customer.FirstName)
                    && (string.IsNullOrEmpty(c.LastName) || c.LastName == customer.LastName)
                    && (string.IsNullOrEmpty(c.AddressLine1) || c.AddressLine1 == customer.AddressLine1)
                    && (string.IsNullOrEmpty(c.AddressPostcode) || c.AddressPostcode == customer.AddressPostcode)
                    )
                {
                    searchResults.Add(customer);
                }
            }

            return searchResults;
        }

        public void Add(Customer c)
        {
            int currentMaxId = 0;
            using (StreamReader sr = new StreamReader(_customerFileLocation))
            {
                // read the header
                sr.ReadLine();

                while(sr.Peek() != -1)
                {
                    string[] splitLine = sr.ReadLine().Split(new[] { ","}, System.StringSplitOptions.RemoveEmptyEntries);
                    int id = System.Convert.ToInt32(splitLine[0]);
                    if (id > currentMaxId)
                    {
                        currentMaxId = id;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(_customerFileLocation, true))
            {
                sw.WriteLine(++currentMaxId + "," + c.Title.Id + "," + c.FirstName + "," + c.LastName + "," + c.AddressLine1 + "," + c.AddressPostcode);
            }
        }

        public void Update(Customer c)
        {
            List<Customer> customers = this.GetAll();
            
            using (StreamWriter sw = new StreamWriter(_customerFileLocation))
            {
                sw.WriteLine("Id,TitleId,FirstName,LastName,AddressLine1,AddressPostcode");

                foreach (var customer in customers)
                {
                    if (customer.Id != c.Id)
                    {
                        sw.WriteLine(customer.Id + "," + customer.Title.Id + "," + customer.FirstName + "," + customer.LastName + "," + customer.AddressLine1 + "," + customer.AddressPostcode);
                    }
                    else
                    {
                        sw.WriteLine(c.Id + "," + c.Title.Id + "," + c.FirstName + "," + c.LastName + "," + c.AddressLine1 + "," + c.AddressPostcode);
                    }                    
                }
            }
        }

        public void Delete(int id)
        {
            List<Customer> customers = this.GetAll();
            
            using (StreamWriter sw = new StreamWriter(_customerFileLocation))
            {
                sw.WriteLine("Id,TitleId,FirstName,LastName,AddressLine1,AddressPostcode");

                foreach (var c in customers)
                {
                    if (c.Id != id)
                    {
                        sw.WriteLine(c.Id + "," + c.Title.Id + "," + c.FirstName + "," + c.LastName + "," + c.AddressLine1 + "," + c.AddressPostcode);
                    }
                }
            }
        }
    }
}