namespace Crawler.Crafting
{
    public interface ICraftingInventoryNodeBingo
    {
        CraftingInventoryBingoResult Execute(CraftingInventoryNodeBingoForm[] forms,
            ICraftingInventory inventory);
    }
}