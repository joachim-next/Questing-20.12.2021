namespace Crawler.Crafting
{
    public interface ICraftingFormationFinder
    {
        ICraftingFormationFindingResult Find(ICraftingInventory inventory);
    }
}