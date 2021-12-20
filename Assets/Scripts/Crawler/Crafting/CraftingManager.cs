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
            var findingResult = _formationFinder.Find(_craftingInventory);
            if (!findingResult.Success)
                return CraftingResult.Failed;

            if (!_formationValidator.Validate(findingResult.Formations))
                return CraftingResult.Failed;
            
            return CraftingResult.Success;
        }
    }
}