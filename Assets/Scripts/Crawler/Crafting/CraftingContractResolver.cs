using System.Linq;
using Crawler.Common;
using Crawler.UI;

namespace Crawler.Crafting
{
    public class CraftingContractResolver : ICraftingContractResolver
    {
        private readonly ICraftingInventory _inventory;
        private readonly ICraftingInventoryNodeBingoFormFactory _bingoFormFactory;
        private readonly ICraftingInventoryNodeBingo _inventoryInventoryNodeBingo;
        
        public CraftingContractResolver(ICraftingInventory inventory, 
            ICraftingInventoryNodeBingoFormFactory bingoFormFactory, ICraftingInventoryNodeBingo inventoryNodeBingo)
        {
            _inventory = inventory;
            _bingoFormFactory = bingoFormFactory;
            _inventoryInventoryNodeBingo = inventoryNodeBingo;
        }
        
        public CraftingContract[] Resolve(CraftingFormation[] formations)
        {
            formations.ThrowIfNull(nameof(formations));
            formations.ThrowIfEmpty(nameof(formations));
            formations.ThrowIfAnyNullEntry(nameof(formations));
            
            var bingoForms = _bingoFormFactory.Create(_inventory, formations);

            var bingoResults = _inventoryInventoryNodeBingo.Execute(bingoForms, _inventory);
            
            return bingoResults
                .Where(x => x.CheckedNodes.All(y => y))
                .Select(Convert)
                .ToArray();
        }

        private CraftingContract Convert(CraftingInventoryNodeBingoForm bingoForm)
        {
            return new CraftingContract(bingoForm.CheckedItems, bingoForm.Formation);
        }
    }
}