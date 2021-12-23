using System.Linq;

namespace Crawler.Crafting
{
    public class CraftingInventoryNodeBingo : ICraftingInventoryNodeBingo
    {
        public CraftingInventoryNodeBingoForm[] Execute(CraftingInventoryNodeBingoForm[] forms,
            ICraftingInventory inventory)
        {
            foreach (var node in inventory.Nodes)
            {
                foreach (var form in forms)
                {
                    form.TryCheck(node);   
                }
            }

            return forms;
        }
    }
}