namespace Crawler.Crafting
{
    public class CraftingManager
    {
        private readonly ICraftingFormationValidator _formationValidator;
        
        public CraftingManager(CraftingInventory craftingInventory, ICraftingFormationValidator formationValidator)
        {
            _formationValidator = formationValidator;
        }
        
        public CraftingResult TryCraft()
        {
            return _formationValidator.FindFormations() ? CraftingResult.Success : CraftingResult.Failed;
        }
    }
}