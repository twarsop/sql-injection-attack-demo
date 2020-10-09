using System.Collections.Generic;

namespace webapp.Models
{
    public class IndexViewModel
    {
        public List<Customer> Customers { get; set; }
        public AddCustomerViewModel SearchDetails { get; set; }
    }
}