using System.Linq;

namespace Crawler.Crafting
{
    public class CraftingInventoryNodeBingo : ICraftingInventoryNodeBingo
    {
        public CraftingInventoryBingoResult Execute(CraftingInventoryNodeBingoForm[] forms,
            ICraftingInventory inventory)
        {
            foreach (var node in inventory.Nodes)
            {
                foreach (var form in forms)
                {
                    form.TryCheck(node.X, node.Y);   
                }
            }

            var checkedFormations = forms
                .Where(x => x.CheckedNodes.All(y => y))
                .Select(x => x.Formation)
                .ToArray();
            
            return new CraftingInventoryBingoResult(checkedFormations);
        }
    }
}