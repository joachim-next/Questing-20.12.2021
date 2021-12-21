namespace Crawler.Crafting
{
    public interface ICraftingInventory
    {
        bool HasItems { get; }
        CraftingInventoryItem[] Nodes { get; }
    }
}