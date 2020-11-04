using System.Collections.Generic;
using DataLayer.Interfaces;
using DataLayer.Models;
using Npgsql;

namespace DataLayer.Repositories.DefendWithParameters
{
    public class CustomerRepository : DataLayer.Repositories.Attack.CustomerRepository, ICustomerRepository
    {
        private class Parameter
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }

        public override Customer Get(int id)
        {
            var selectSql = "SELECT id, titleid, firstname, lastname, addressline1, addresspostcode FROM " + this._tablestr + " WHERE id = @id";

            var customer = new Customer();

            var conn = new NpgsqlConnection(this._connstr);
            conn.Open();

            using (var cmd = new NpgsqlCommand(selectSql, conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customer = this.ParseCustomerFromReader(reader);
                }
            }

            conn.Close();

            return customer;
        }

        public override List<Customer> Search(Customer c)
        {
            var searchTerms = new Dictionary<string, SearchTermValue>();
            
            AppendSearchTermValue(searchTerms, "titleid", c.Title.Id);
            AppendSearchTermValue(searchTerms, "firstname", c.FirstName);
            AppendSearchTermValue(searchTerms, "lastname", c.LastName);
            AppendSearchTermValue(searchTerms, "addressline1", c.AddressLine1);
            AppendSearchTermValue(searchTerms, "addresspostcode", c.AddressPostcode);

            var parameters = new List<Parameter>();

            var searchSql = "SELECT id, titleid, firstname, lastname, addressline1, addresspostcode FROM " + this._tablestr;

            if (searchTerms.Count > 0)
            {
                searchSql += " WHERE ";

                foreach (var key in searchTerms.Keys)
                {
                    searchSql += key + " = @" + key;
                    searchSql += " AND ";

                    parameters.Add(new Parameter{ Name = key, Value = searchTerms[key].CastValueByType() });
                }

                searchSql = searchSql.Substring(0, searchSql.Length-5);
                searchSql += ";";
            }            

            List<Customer> customers = new List<Customer>();

            var conn = new NpgsqlConnection(this._connstr);
            conn.Open();

            using (var cmd = new NpgsqlCommand(searchSql, conn))
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Name, parameter.Value);
                }
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(this.ParseCustomerFromReader(reader));
                }
            }

            conn.Close();

            return customers;
        }

        public override void Add(Customer c)
        {
            var insertSql = "INSERT INTO " + this._tablestr + " (titleid, firstname, lastname, addressline1, addresspostcode) VALUES ";
            insertSql += "(@titleid, @firstname, @lastname, @addressline1, @addresspostcode);";

            this.ExecuteNonQueryWithParameters(insertSql, GetParametersFromCustomer(c));
        }

        public override void Update(Customer c)
        {
            var updateSql = "UPDATE " + this._tablestr + " SET ";
            updateSql += "titleid = @titleid, ";
            updateSql += "firstname = @firstname, ";
            updateSql += "lastname = @lastname, ";
            updateSql += "addressline1 = @addressline1, ";
            updateSql += "addresspostcode = @addresspostcode ";
            updateSql += "WHERE id = @id";

            var parameters = GetParametersFromCustomer(c);
            parameters.Add( new Parameter { Name = "id", Value = c.Id });

            this.ExecuteNonQueryWithParameters(updateSql, parameters);
        }

        public override void Delete(int id)
        {
            var deleteSql = "DELETE FROM " + this._tablestr + " WHERE id = @id;";
            
            this.ExecuteNonQueryWithParameters(deleteSql, new List<Parameter> { new Parameter { Name = "id", Value = id } });
        }

        private List<Parameter> GetParametersFromCustomer(Customer c)
        {
            return new List<Parameter>
            {
                new Parameter { Name = "titleid", Value = c.Title.Id },
                new Parameter { Name = "firstname", Value = c.FirstName },
                new Parameter { Name = "lastname", Value = c.LastName },
                new Parameter { Name = "addressline1", Value = c.AddressLine1 },
                new Parameter { Name = "addresspostcode", Value = c.AddressPostcode }
            };
        }

        private void ExecuteNonQueryWithParameters(string sql, List<Parameter> parameters)
        {
            var conn = new NpgsqlConnection(this._connstr);
            conn.Open();

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Name, parameter.Value);
                }
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }
    }
}