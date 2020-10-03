using System.Collections.Generic;
using datalayer.models;

namespace datalayer.interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
    }    
}