using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models
{
    public class Assortment
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }


    }
}
    