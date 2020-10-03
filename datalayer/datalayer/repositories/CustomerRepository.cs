using System.Collections.Generic;
using System.IO;
using datalayer.interfaces;
using datalayer.models;

namespace datalayer.repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            using (StreamReader sr = new StreamReader(@"C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\datalayer\datalayer\data\customers.csv"))
            {
                // read the header
                sr.ReadLine();

                while(sr.Peek() != -1)
                {
                    string[] splitLine = sr.ReadLine().Split(new[] { ","}, System.StringSplitOptions.RemoveEmptyEntries);
                    customers.Add(new Customer
                    {
                        Id = System.Convert.ToInt32(splitLine[0]),
                        Title = splitLine[1],
                        FirstName = splitLine[2],
                        LastName = splitLine[3],
                        AddressLine1 = splitLine[4],
                        AddressPostcode = splitLine[5]
                    });
                }
            }

            return customers;
        }
    }
}