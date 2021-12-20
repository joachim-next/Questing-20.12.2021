namespace Crawler.Crafting
{
    public class CraftingFormationFinder : ICraftingFormationFinder
    {
        public CraftingFormationFindingResult Find(ICraftingInventory inventory)
        {
            return new CraftingFormationFindingResult(true, new CraftingFormation[1]);
        }
    }
}