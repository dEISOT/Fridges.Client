using Fridges.Client.Models;

namespace Fridges.Client.ViewModels
{
    public class FridgeViewModel
    {
        public IEnumerable<Fridge> Fridges { get; set; }
        public IEnumerable<FridgeType> Types { get; set; }
    }
}
