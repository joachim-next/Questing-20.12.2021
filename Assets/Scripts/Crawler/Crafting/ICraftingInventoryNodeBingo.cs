namespace Crawler.Crafting
{
    public interface ICraftingInventoryNodeBingo
    {
        CraftingInventoryNodeBingoForm[] Execute(CraftingInventoryNodeBingoForm[] forms,
            ICraftingInventory inventory);
    }
}