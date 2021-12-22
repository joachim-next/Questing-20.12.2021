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

        public override bool Equals(object obj)
        {
            if (obj == null ||
                obj.GetType() != GetType())
            {
                return false;
            }

            var target = (CraftingInventoryItem) obj;

            if (target.IngredientType != IngredientType ||
                target.X != X ||
                target.Y != Y)
            {
                return false;
            }
            
            return true;
        }

        public override int GetHashCode()
        {
            return IngredientType ^ X ^ Y;
        }
    }
}