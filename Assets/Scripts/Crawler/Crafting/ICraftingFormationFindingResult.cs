namespace Crawler.Crafting
{
    public interface ICraftingFormationFindingResult
    {
        bool Success { get; }
        CraftingFormation[] Formations { get; }
    }
}