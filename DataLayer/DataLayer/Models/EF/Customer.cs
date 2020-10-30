using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.EF
{
    public class Customer
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("titleid")]
        public int TitleId { get; set; }
        [Column("firstname")]
        public string FirstName { get; set; }
        [Column("lastname")]
        public string LastName { get; set; }
        [Column("addressline1")]
        public string AddressLine1 { get; set; }
        [Column("addresspostcode")]
        public string AddressPostcode { get; set; }

        public Title Title { get; set; }
    }    
}