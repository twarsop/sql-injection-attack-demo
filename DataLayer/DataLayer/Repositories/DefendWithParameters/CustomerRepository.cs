using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using DataLayer.Interfaces;
using DataLayer.Models;
using Npgsql;

namespace DataLayer.Repositories.DefendWithParameters
{
    public class CustomerRepository : DataLayer.Repositories.Attack.CustomerRepository, ICustomerRepository
    {
        public override void Add(Customer c)
        {
            var insertSql = "INSERT INTO " + this._tablestr + " (titleid, firstname, lastname, addressline1, addresspostcode) VALUES ";
            insertSql += "(@titleid, @firstname, @lastname, @addressline1, @addresspostcode);";

            var conn = new NpgsqlConnection(this._connstr);
            conn.Open();

            using (var cmd = new NpgsqlCommand(insertSql, conn))
            {
                cmd.Parameters.AddWithValue("titleid", c.Title.Id);
                cmd.Parameters.AddWithValue("firstname", c.FirstName);
                cmd.Parameters.AddWithValue("lastname", c.LastName);
                cmd.Parameters.AddWithValue("addressline1", c.AddressLine1);
                cmd.Parameters.AddWithValue("addresspostcode", c.AddressPostcode);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }
    }
}