using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models
{
    public class Fridge
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TypeId { get; set; }
    }
}
