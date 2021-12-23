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
        
        public CraftingContract[] TryGetContracts()
        {
            var findingResult = _formationFinder.Find(_craftingInventory);
            if (!findingResult.Success)
                return new CraftingContract[0];

            if (!_formationValidator.Validate(findingResult.Formations))
                return new CraftingContract[0];
            
            return new CraftingContract[1];
        }
    }
}