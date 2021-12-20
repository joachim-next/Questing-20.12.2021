namespace Crawler.Crafting
{
    public class CraftingFormation
    {
        public CraftingFormationNode[] Nodes { get; }

        public CraftingFormation(CraftingFormationNode[] nodes)
        {
            Nodes = nodes;
        }
    }
}