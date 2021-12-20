namespace Crawler.Crafting
{
    public class CraftingManager
    {
        private readonly ICraftingInventory _craftingInventory;
        private readonly ICraftingFormationValidator _formationValidator;
        
        public CraftingManager(ICraftingInventory craftingInventory, ICraftingFormationValidator formationValidator)
        {
            _craftingInventory = craftingInventory;
            _formationValidator = formationValidator;
        }
        
        public CraftingResult TryCraft()
        {
            if (!_craftingInventory.HasItems)
                return CraftingResult.Failed;
            
            return _formationValidator.FindFormations() ? CraftingResult.Success : CraftingResult.Failed;
        }
    }
}