namespace Crawler.Crafting
{
    public class CraftingManager
    {
        public CraftingManager(Inventory inventory, ICraftingFormationValidator formationValidator)
        {
            
        }
        
        public CraftingResult TryCraft()
        {
            return CraftingResult.Failed;
        }
    }
}