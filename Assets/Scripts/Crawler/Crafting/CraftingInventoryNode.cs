namespace Crawler.Crafting
{
    public class CraftingInventoryNode
    {
        public int IngredientType { get; }
        public int X { get; }
        public int Y { get; }

        public CraftingInventoryNode(int ingredientType, int x, int y)
        {
            IngredientType = ingredientType;
            X = x;
            Y = y;
        }
    }
}