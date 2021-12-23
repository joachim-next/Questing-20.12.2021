namespace Crawler.Crafting
{
    public interface ICraftingContractResolver
    {
        CraftingContract[] Resolve(CraftingFormation[] formations);
    }
}