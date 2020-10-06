using System.Collections.Generic;
using System.IO;
using datalayer.interfaces;
    using datalayer.models;

namespace datalayer.repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ITitleRepository _titleRepository;

        public CustomerRepository()
        {
            _titleRepository = new TitleRepository();
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

        public void Add(Customer c)
        {
            int currentMaxId = 0;
            using (StreamReader sr = new StreamReader(@"C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\datalayer\datalayer\data\customers.csv"))
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

            using (StreamWriter sw = new StreamWriter(@"C:\Users\t_war\Documents\projects\code\sql-injection-attack-demo\datalayer\datalayer\data\customers.csv", true))
            {
                sw.WriteLine(++currentMaxId + "," + c.Title.Id + "," + c.FirstName + "," + c.LastName + "," + c.AddressLine1 + "," + c.AddressPostcode);
            }
        }
    }
}