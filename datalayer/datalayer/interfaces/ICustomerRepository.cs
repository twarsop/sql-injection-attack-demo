using System.Collections.Generic;
using datalayer.models;

namespace datalayer.interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        List<Customer> Search(Customer c);
        void Add(Customer c);
        void Delete(int id);
    }    
}