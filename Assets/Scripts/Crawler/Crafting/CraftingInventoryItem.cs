namespace Crawler.Crafting
{
    public class CraftingInventoryItem
    {
        public int IngredientType { get; }
        public int X { get; }
        public int Y { get; }

        public CraftingInventoryItem(int ingredientType, int x, int y)
        {
            IngredientType = ingredientType;
            X = x;
            Y = y;
        }
    }
}