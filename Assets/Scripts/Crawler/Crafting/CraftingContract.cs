namespace Crawler.Crafting
{
    public class CraftingContract
    {
        public CraftingInventoryItem ItemToBeCrafted { get; }
        public CraftingInventoryItem[] ItemsToBeUsed { get; }
        public CraftingFormation Formation { get; }
     
        public CraftingContract(CraftingInventoryItem itemToBeCrafted, CraftingInventoryItem[] itemsToBeUsed,
            CraftingFormation formation)
        {
            ItemToBeCrafted = itemToBeCrafted;
            ItemsToBeUsed = itemsToBeUsed;
            Formation = formation;
        }
    }
}