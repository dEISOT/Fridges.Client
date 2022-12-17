namespace Fridges.Client.Models
{
    public class AssortmentPost
    {
        public Guid FridgeId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
