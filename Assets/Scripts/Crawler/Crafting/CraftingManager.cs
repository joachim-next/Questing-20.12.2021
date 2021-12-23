namespace Crawler.Crafting
{
    public class CraftingManager
    {
        private readonly ICraftingInventory _craftingInventory;
        private readonly ICraftingFormationFinder _formationFinder;
        private readonly ICraftingContractResolver _contractResolver;
        
        public CraftingManager(ICraftingInventory craftingInventory, ICraftingFormationFinder formationFinder,
            ICraftingContractResolver contractResolver)
        {
            _craftingInventory = craftingInventory;
            _formationFinder = formationFinder;
            _contractResolver = contractResolver;
        }
        
        public CraftingContract[] TryGetContracts()
        {
            var findingResult = _formationFinder.Find(_craftingInventory);
            if (!findingResult.Success)
                return new CraftingContract[0];

            var contracts = _contractResolver.Resolve(findingResult.Formations); 
            if (contracts.Length == 0)
                return new CraftingContract[0];
            
            return new CraftingContract[1];
        }
    }
}