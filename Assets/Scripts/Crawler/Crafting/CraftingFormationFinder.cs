using System.Collections.Generic;
using System.Linq;

namespace Crawler.Crafting
{
    public class CraftingFormationFinder : ICraftingFormationFinder
    {
        private readonly ICraftingFormationProvider _formationProvider;
        private readonly ICraftingFormationFilter _formationFilter;
        private readonly ICraftingInventoryNodeBingoFormFactory _bingoFormFactory;
        private readonly ICraftingInventoryNodeBingo _inventoryNodeBingo;

        public CraftingFormationFinder(ICraftingFormationProvider formationProvider,
            ICraftingFormationFilter formationFilter, ICraftingInventoryNodeBingoFormFactory bingoFormFactory, 
            ICraftingInventoryNodeBingo inventoryNodeBingo)
        {
            _formationProvider = formationProvider;
            _formationFilter = formationFilter;
            _bingoFormFactory = bingoFormFactory;
            _inventoryNodeBingo = inventoryNodeBingo;
        }
        
        public CraftingFormationFindingResult Find(ICraftingInventory inventory)
        {
            if(!inventory.HasItems)
                return new CraftingFormationFindingResult(false, new CraftingFormation[0]);

            var formations = _formationProvider.Provide();

            formations = _formationFilter.Execute(formations, inventory);

            var bingoForms = _bingoFormFactory.Create(inventory, formations);

            var bingoResults = _inventoryNodeBingo.Execute(bingoForms, inventory);
            
            return new CraftingFormationFindingResult(true, bingoResults.Formations);
        }
    }
}