namespace DataLayer.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public Title Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressPostcode { get; set; }
    }    
}