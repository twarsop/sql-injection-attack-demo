using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class CustomerViewModel
    {
        public List<Title> Titles { get; set; }
        public Customer Customer { get; set; }
        public int CustomerTitleId { get; set; }
    }
}