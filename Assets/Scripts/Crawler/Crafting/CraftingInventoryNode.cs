namespace Crawler.Crafting
{
    public class CraftingInventoryNode
    {
        public int IngredientType { get; }

        public CraftingInventoryNode(int ingredientType)
        {
            IngredientType = ingredientType;
        }
    }
}