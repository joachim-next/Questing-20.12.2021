namespace Crawler.Crafting
{
    public interface ICraftingFormationFilter
    {
        CraftingFormation[] Execute(CraftingFormation[] formations, ICraftingInventory inventory);
    }
}