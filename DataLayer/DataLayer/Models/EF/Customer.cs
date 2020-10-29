namespace DataLayer.Models.EF
{
    public class Customer
    {
        public int id { get; set; }
        public int titleid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string addressline1 { get; set; }
        public string addresspostcode { get; set; }
    }    
}