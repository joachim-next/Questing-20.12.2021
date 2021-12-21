namespace Crawler.Crafting
{
    public class CraftingFormationNode
    {
        public int IngredientType { get; }
        public int X { get; }
        public int Y { get; }

        public CraftingFormationNode(int ingredientType, int x, int y)
        {
            IngredientType = ingredientType;
            X = x;
            Y = y;
        }
    }
}