namespace Crawler.Crafting
{
    public class CraftingInventoryBingoResult
    {
        public CraftingFormation[] Formations { get; }

        public CraftingInventoryBingoResult(CraftingFormation[] formations)
        {
            Formations = formations;
        }
    }
}