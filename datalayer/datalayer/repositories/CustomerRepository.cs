using System.Collections.Generic;
using datalayer.interfaces;
using datalayer.models;

namespace datalayer.repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public IEnumerable<Customer> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}