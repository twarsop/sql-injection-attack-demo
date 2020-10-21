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
            var insertSql = "INSERT INTO public.customers (titleid, firstname, lastname, addressline1, addresspostcode) VALUES ";
            insertSql += "(" + c.Title.Id.ToString() + ", ";
            insertSql += "'" + c.FirstName + "', ";
            insertSql += "'" + c.LastName + "', ";
            insertSql += "'" + c.AddressLine1 + "', ";
            insertSql += "'" + c.AddressPostcode + "');";

            this.ExecuteNonQuery(insertSql);
        }

        public void Update(Customer c)
        {
            var updateSql = "UPDATE public.customers SET ";
            updateSql += "titleid = " + c.Title.Id.ToString() + ", ";
            updateSql += "firstname = '" + c.FirstName + "', ";
            updateSql += "lastname = '" + c.LastName + "', ";
            updateSql += "addressline1 = '" + c.AddressLine1 + "', ";
            updateSql += "addresspostcode = '" + c.AddressPostcode + "' ";
            updateSql += "WHERE id = " + c.Id.ToString();

            this.ExecuteNonQuery(updateSql);
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

        private void ExecuteNonQuery(string sql)
        {
            var conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo");
            conn.Open();

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }
    }
}