using System.Collections.Generic;
using System.IO;
using datalayer.interfaces;
using datalayer.models;
using Npgsql;

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
            var customer = new Customer();

            var conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo");
            conn.Open();

            using (var command = new NpgsqlCommand("SELECT id, titleid, firstname, lastname, addressline1, addresspostcode FROM public.customers WHERE id = " + id.ToString(), conn))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    customer = this.ParseCustomerFromReader(reader);
                }
            }

            conn.Close();

            return customer;
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            var conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo");
            conn.Open();

            using (var command = new NpgsqlCommand("SELECT id, titleid, firstname, lastname, addressline1, addresspostcode FROM public.customers", conn))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(this.ParseCustomerFromReader(reader));
                }
            }

            conn.Close();

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

        private Customer ParseCustomerFromReader(NpgsqlDataReader reader)
        {
            List<Title> titles = _titleRepository.GetAll();
            var titleLookup = new Dictionary<int, Title>();
            foreach (Title title in titles)
            {
                titleLookup.Add(title.Id, title);
            }

            return new Customer
            {
                Id = System.Int32.Parse(reader["id"].ToString()),
                Title = titleLookup[System.Int32.Parse(reader["titleid"].ToString())],
                FirstName = reader["firstname"].ToString(),
                LastName = reader["lastname"].ToString(),
                AddressLine1 = reader["addressline1"].ToString(),
                AddressPostcode = reader["addresspostcode"].ToString()
            };
        }
    }
}