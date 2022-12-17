namespace Fridges.Client.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Fridge> Fridges { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
