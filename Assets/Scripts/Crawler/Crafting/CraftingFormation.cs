namespace Crawler.Crafting
{
    public class CraftingFormation
    {
        public int ResultItemIngredientType;
        public CraftingFormationNode[] Nodes { get; }

        public CraftingFormation(int resultItemIngredientType, CraftingFormationNode[] nodes)
        {
            ResultItemIngredientType = resultItemIngredientType;
            Nodes = nodes;
        }
    }
}