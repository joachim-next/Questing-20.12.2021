namespace Crawler.Crafting
{
    public class CraftingManager
    {
        private readonly ICraftingInventory _craftingInventory;
        private readonly ICraftingFormationFinder _formationFinder;
        
        public CraftingManager(ICraftingInventory craftingInventory, ICraftingFormationFinder formationFinder)
        {
            _craftingInventory = craftingInventory;
            _formationFinder = formationFinder;
        }
        
        public CraftingResult TryCraft()
        {
            if (!_craftingInventory.HasItems)
                return CraftingResult.Failed;
            
            return _formationFinder.FindFormations() ? CraftingResult.Success : CraftingResult.Failed;
        }
    }
}