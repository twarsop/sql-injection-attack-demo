using System.Collections.Generic;
using System.Web.Mvc;

namespace webapp.Models
{
    public class EditCustomerViewModel
    {
        public List<Title> Titles { get; set; }
        public Customer Customer { get; set; }
        public int CustomerTitleId { get; set; }
    }
}