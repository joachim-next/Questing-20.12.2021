namespace Crawler.Crafting
{
    public interface ICraftingFormationFinder
    {
        CraftingFormationFindingResult Find(ICraftingInventory inventory);
    }
}