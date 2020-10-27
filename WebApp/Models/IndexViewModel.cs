using System.Collections.Generic;

namespace WebApp.Models
{
    public class IndexViewModel
    {
        public List<Customer> Customers { get; set; }
        public CustomerViewModel SearchDetails { get; set; }
    }
}