using System.Collections.Generic;

namespace Crawler.Crafting
{
    public class CraftingInventory : ICraftingInventory
    {
        public bool HasItems => Nodes.Count != 0; 
        public List<CraftingInventoryNode> Nodes { get; }
        
        public CraftingInventory(List<CraftingInventoryNode> nodes)
        {
            Nodes = nodes;
        }
    }
}