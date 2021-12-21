using System.Collections.Generic;

namespace Crawler.Crafting
{
    public class CraftingInventory : ICraftingInventory
    {
        public bool HasItems => Nodes.Length != 0; 
        public CraftingInventoryItem[] Nodes { get; }
        
        public CraftingInventory(CraftingInventoryItem[] nodes)
        {
            Nodes = nodes;
        }
    }
}