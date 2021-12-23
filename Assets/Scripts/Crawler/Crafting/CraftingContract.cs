namespace Crawler.Crafting
{
    public class CraftingContract
    {
        public CraftingInventoryItem[] ItemsToBeUsed { get; }
        public CraftingFormation Formation { get; }
     
        public CraftingContract(CraftingInventoryItem[] itemsToBeUsed, CraftingFormation formation)
        {
            ItemsToBeUsed = itemsToBeUsed;
            Formation = formation;
        }
    }
}