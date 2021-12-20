namespace Crawler.Crafting
{
    public class CraftingFormationNode
    {
        public int IngredientType { get; }

        public CraftingFormationNode(int ingredientType)
        {
            IngredientType = ingredientType;
        }
    }
}