namespace Crawler.Crafting
{
    public class CraftingManager
    {
        private readonly ICraftingInventory _craftingInventory;
        private readonly ICraftingFormationFinder _formationFinder;
        private readonly ICraftingFormationValidator _formationValidator;
        
        public CraftingManager(ICraftingInventory craftingInventory, ICraftingFormationFinder formationFinder,
            ICraftingFormationValidator formationValidator)
        {
            _craftingInventory = craftingInventory;
            _formationFinder = formationFinder;
            _formationValidator = formationValidator;
        }
        
        public CraftingResult TryCraft()
        {
            if (!_craftingInventory.HasItems)
                return CraftingResult.Failed;

            var formations = _formationFinder.Find();
            if (formations.Length == 0)
                return CraftingResult.Failed;

            if (!_formationValidator.Validate(formations))
                return CraftingResult.Failed;
            
            return CraftingResult.Success;
        }
    }
}