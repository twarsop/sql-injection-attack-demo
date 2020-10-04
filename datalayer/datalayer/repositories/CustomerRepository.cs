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
            // preload the list of titles and store in dictionary for later use
            Dictionary<int, Title> titleLookup = new Dictionary<int, Title>();
            using (StreamReader sr = new StreamReader(@"C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\datalayer\datalayer\data\titles.csv"))
            {
                // read the header
                sr.ReadLine();

                while (sr.Peek() != -1)
                {
                    string[] splitLine = sr.ReadLine().Split(new[] { ","}, System.StringSplitOptions.RemoveEmptyEntries);
                    int id = System.Convert.ToInt32(splitLine[0]);
                    titleLookup.Add(id, new Title { Id = id, Name = splitLine[1] });
                }
            }

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
    }
}