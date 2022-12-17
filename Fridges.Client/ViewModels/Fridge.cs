using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models
{
    public class Fridge
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public string Type { get; set; }
    }
}
