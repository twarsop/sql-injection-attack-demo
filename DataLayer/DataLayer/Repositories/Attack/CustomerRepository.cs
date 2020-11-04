using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using DataLayer.Interfaces;
using DataLayer.Models;
using Npgsql;

namespace DataLayer.Repositories.Attack
{
    public class CustomerRepository : ICustomerRepository
    {
        public class SearchTermValue
        {
            public string Value { get; set; }
            public SearchTermType Type { get; set; }

            public object CastValueByType()
            {
                if (this.Type == SearchTermType.Numeric)
                {
                    return System.Convert.ToInt32(this.Value);
                }
                return this.Value;
            }
        }

        public enum SearchTermType
        {
            Numeric,
            String
        }

        private readonly ITitleRepository _titleRepository;
        protected readonly string _connstr;
        protected readonly string _tablestr;

        public CustomerRepository()
        {
            _titleRepository = new TitleRepository();
            
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();            
            _connstr = configuration.GetValue<string>("ConnStr");

            _tablestr = "public.customers";
        }

        public virtual Customer Get(int id)
        {
            var selectSql = "SELECT id, titleid, firstname, lastname, addressline1, addresspostcode FROM " + this._tablestr + " WHERE id = " + id.ToString();

            var customer = new Customer();

            var conn = new NpgsqlConnection(this._connstr);
            conn.Open();

            using (var cmd = new NpgsqlCommand(selectSql, conn))
            {
                var reader = cmd.ExecuteReader();
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
            var selectSql = "SELECT id, titleid, firstname, lastname, addressline1, addresspostcode FROM " + this._tablestr + ";";

            List<Customer> customers = new List<Customer>();

            var conn = new NpgsqlConnection(this._connstr);
            conn.Open();

            using (var cmd = new NpgsqlCommand(selectSql, conn))
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(this.ParseCustomerFromReader(reader));
                }
            }

            conn.Close();

            return customers;
        }

        public virtual List<Customer> Search(Customer c)
        {
            var searchTerms = new Dictionary<string, SearchTermValue>();
            
            AppendSearchTermValue(searchTerms, "titleid", c.Title.Id);
            AppendSearchTermValue(searchTerms, "firstname", c.FirstName);
            AppendSearchTermValue(searchTerms, "lastname", c.LastName);
            AppendSearchTermValue(searchTerms, "addressline1", c.AddressLine1);
            AppendSearchTermValue(searchTerms, "addresspostcode", c.AddressPostcode);

            var searchSql = "SELECT id, titleid, firstname, lastname, addressline1, addresspostcode FROM " + this._tablestr;

            if (searchTerms.Count > 0)
            {
                searchSql += " WHERE ";

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
            }            

            List<Customer> customers = new List<Customer>();

            var conn = new NpgsqlConnection(this._connstr);
            conn.Open();

            using (var cmd = new NpgsqlCommand(searchSql, conn))
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(this.ParseCustomerFromReader(reader));
                }
            }

            conn.Close();

            return customers;
        }

        protected void AppendSearchTermValue(Dictionary<string, SearchTermValue> searchTerms, string key, int value)
        {
            if (value != 0)
            {
                searchTerms.Add(key, new SearchTermValue { Value = value.ToString(), Type = SearchTermType.Numeric });
            }
        }

        protected void AppendSearchTermValue(Dictionary<string, SearchTermValue> searchTerms, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                searchTerms.Add(key, new SearchTermValue { Value = value, Type = SearchTermType.String });
            }
        }

        public virtual void Add(Customer c)
        {
            var insertSql = "INSERT INTO " + this._tablestr + " (titleid, firstname, lastname, addressline1, addresspostcode) VALUES ";
            insertSql += "(" + c.Title.Id.ToString() + ", ";
            insertSql += "'" + c.FirstName + "', ";
            insertSql += "'" + c.LastName + "', ";
            insertSql += "'" + c.AddressLine1 + "', ";
            insertSql += "'" + c.AddressPostcode + "');";

            this.ExecuteNonQuery(insertSql);
        }

        public virtual void Update(Customer c)
        {
            var updateSql = "UPDATE " + this._tablestr + " SET ";
            updateSql += "titleid = " + c.Title.Id.ToString() + ", ";
            updateSql += "firstname = '" + c.FirstName + "', ";
            updateSql += "lastname = '" + c.LastName + "', ";
            updateSql += "addressline1 = '" + c.AddressLine1 + "', ";
            updateSql += "addresspostcode = '" + c.AddressPostcode + "' ";
            updateSql += "WHERE id = " + c.Id.ToString();

            this.ExecuteNonQuery(updateSql);
        }

        public virtual void Delete(int id)
        {
            var deleteSql = "DELETE FROM " + this._tablestr + " WHERE id = " + id.ToString();
            this.ExecuteNonQuery(deleteSql);
        }

        protected Customer ParseCustomerFromReader(NpgsqlDataReader reader)
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

        protected virtual void ExecuteNonQuery(string sql)
        {
            var conn = new NpgsqlConnection(this._connstr);
            conn.Open();

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }
    }
}