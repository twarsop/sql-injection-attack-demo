using System.Collections.Generic;
using System.Web.Mvc;

namespace webapp.Models
{
    public class AddCustomerViewModel
    {
        public List<Title> Titles { get; set; }
        public Customer Customer { get; set; }
        public int CustomerTitleId { get; set; }
    }
}