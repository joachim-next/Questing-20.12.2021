namespace Crawler.Crafting
{
    public class CraftingFormationFinder : ICraftingFormationFinder
    {
        private readonly ICraftingFormationProvider _formationProvider;

        public CraftingFormationFinder(ICraftingFormationProvider formationProvider)
        {
            _formationProvider = formationProvider;
        }
        
        public CraftingFormationFindingResult Find(ICraftingInventory inventory)
        {
            if(!inventory.HasItems)
                return new CraftingFormationFindingResult(false, new CraftingFormation[0]);
            
            return new CraftingFormationFindingResult(true, new CraftingFormation[1]);
        }
    }
}