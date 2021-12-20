using System.Collections.Generic;

namespace Crawler.Crafting
{
    public interface ICraftingInventory
    {
        bool HasItems { get; }
        List<CraftingInventoryNode> Nodes { get; }
    }
}