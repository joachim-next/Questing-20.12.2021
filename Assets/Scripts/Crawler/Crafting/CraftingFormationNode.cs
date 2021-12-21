namespace Crawler.Crafting
{
    public class CraftingFormationNode
    {
        public int IngredientType { get; }
        public int RelativeX { get; }
        public int RelativeY { get; }

        public CraftingFormationNode(int ingredientType, int relativeX, int relativeY)
        {
            IngredientType = ingredientType;
            RelativeX = relativeX;
            RelativeY = relativeY;
        }
    }
}