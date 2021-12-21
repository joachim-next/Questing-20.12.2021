namespace Crawler.UI.Crafting
{
    public class CraftingInventoryItemViewModel
    {
        public int IngredientType { get; }
        public int X { get; }
        public int Y { get; }
        
        public CraftingInventoryItemViewModel(int ingredientType, int x, int y)
        {
            IngredientType = ingredientType;
            X = x;
            Y = y;
        }
    }
}