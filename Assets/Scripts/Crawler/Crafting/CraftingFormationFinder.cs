namespace Crawler.Crafting
{
    public class CraftingFormationFinder : ICraftingFormationFinder
    {
        private readonly ICraftingFormationProvider _formationProvider;
        private readonly ICraftingFormationFilter _formationFilter;

        public CraftingFormationFinder(ICraftingFormationProvider formationProvider,
            ICraftingFormationFilter formationFilter)
        {
            _formationProvider = formationProvider;
            _formationFilter = formationFilter;
        }
        
        public CraftingFormationFindingResult Find(ICraftingInventory inventory)
        {
            if(!inventory.HasItems)
                return new CraftingFormationFindingResult(false, new CraftingFormation[0]);

            var formations = _formationProvider.Provide();

            formations = _formationFilter.Execute(formations, inventory);
            
            return new CraftingFormationFindingResult(true, formations);
        }
    }
}