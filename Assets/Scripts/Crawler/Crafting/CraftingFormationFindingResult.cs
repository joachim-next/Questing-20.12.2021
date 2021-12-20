namespace Crawler.Crafting
{
    public class CraftingFormationFindingResult
    {
        public bool Success { get; }
        public CraftingFormation[] Formations { get; }

        public CraftingFormationFindingResult(bool success, CraftingFormation[] formations)
        {
            Success = success;
            Formations = formations;
        }
    }
}