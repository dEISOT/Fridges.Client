namespace Fridges.Client.Models
{
    public class AssortmentWithProduct
    {
        public IEnumerable<Assortment> AssortmentList { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
    }
}
