using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class Title
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }    
}