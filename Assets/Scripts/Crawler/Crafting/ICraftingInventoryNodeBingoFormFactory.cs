namespace Crawler.Crafting
{
    public interface ICraftingInventoryNodeBingoFormFactory
    {
        CraftingInventoryNodeBingoForm[] Create(ICraftingInventory inventory, CraftingFormation[] formations);
    }
}