using System.Collections.Generic;
using datalayer.models;

namespace datalayer.interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
    }    
}