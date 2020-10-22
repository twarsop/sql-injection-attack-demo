using System.Collections.Generic;
using System.IO;
using datalayer.interfaces;
using datalayer.models;
using Npgsql;

namespace datalayer.repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public class SearchTermValue
        {
            public string Value { get; set; }
            public SearchTermType Type { get; set; }
        }

        public enum SearchTermType
        {
            Numeric,
            String
        }

        private readonly ITitleRepository _titleRepository;

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

            using (var command = new NpgsqlCommand("SELECT id, titleid, firstname, lastname, addressline1, addresspostcode FROM public.customers;", conn))
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
            var searchTerms = new Dictionary<string, SearchTermValue>();
            
            AppendSearchTermValue(searchTerms, "titleid", c.Title.Id);
            AppendSearchTermValue(searchTerms, "firstname", c.FirstName);
            AppendSearchTermValue(searchTerms, "lastname", c.LastName);
            AppendSearchTermValue(searchTerms, "addressline1", c.AddressLine1);
            AppendSearchTermValue(searchTerms, "addresspostcode", c.AddressPostcode);

            var searchSql = "SELECT id, titleid, firstname, lastname, addressline1, addresspostcode FROM public.customers";

            if (searchTerms.Count > 0)
            {
                searchSql += " WHERE ";
            }

            foreach (var key in searchTerms.Keys)
            {
                searchSql += key + "=";

                if (searchTerms[key].Type == SearchTermType.Numeric)
                {
                    searchSql += searchTerms[key].Value;
                }
                else if (searchTerms[key].Type == SearchTermType.String)
                {
                    searchSql += "'" + searchTerms[key].Value + "'";
                }

                searchSql += " AND ";
            }

            searchSql = searchSql.Substring(0, searchSql.Length-5);
            searchSql += ";";

            List<Customer> customers = new List<Customer>();

            var conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo");
            conn.Open();

            using (var command = new NpgsqlCommand(searchSql, conn))
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

        private void AppendSearchTermValue(Dictionary<string, SearchTermValue> searchTerms, string key, int value)
        {
            if (value != 0)
            {
                searchTerms.Add(key, new SearchTermValue { Value = value.ToString(), Type = SearchTermType.Numeric });
            }
        }

        private void AppendSearchTermValue(Dictionary<string, SearchTermValue> searchTerms, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                searchTerms.Add(key, new SearchTermValue { Value = value, Type = SearchTermType.String });
            }
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
            var deleteSql = "DELETE FROM public.customers WHERE id = " + id.ToString();
            this.ExecuteNonQuery(deleteSql);
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