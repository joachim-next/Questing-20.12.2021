using System.Collections.Generic;
using System.Linq;

namespace Crawler.Crafting
{
    public class NotOwnedFormationFilter
    {
        public CraftingFormation[] Filter(CraftingFormation[] formations, 
            ICraftingInventory inventory)
        {
            var redundantFormationIndices = IndicesOfFormationsWithNotOwnedIngredients(formations, inventory);

            var formationsList = new List<CraftingFormation>(formations);
            foreach (var redundantFormationIndex in redundantFormationIndices)
            {
                formationsList.RemoveAt(redundantFormationIndex);
            }
            
            return formationsList.ToArray();
        }

        private int[] IndicesOfFormationsWithNotOwnedIngredients(CraftingFormation[] formations, 
            ICraftingInventory inventory)
        {
            var indices = new List<int>();
            
            for (int i = 0; i < formations.Length; i++)
            {
                if(IngredientsForFormationExist(formations[i], inventory))
                    continue;
                
                indices.Add(i);
            }

            return indices.ToArray();
        }

        private bool IngredientsForFormationExist(CraftingFormation formation, ICraftingInventory inventory)
        {
            var formationIngredientTypeToCountMap = formation.Nodes
                .GroupBy(x => x.IngredientType);

            foreach (var group in formationIngredientTypeToCountMap)
            {
                if (!HasEnoughOfIngredient(group.Key, group.Count(), inventory))
                    return false;
            }

            return true;
        }

        private bool HasEnoughOfIngredient(int ingredientType, int neededCount, ICraftingInventory inventory)
        {
            var ownedCount = inventory.Nodes
                .Count(x => x.IngredientType == ingredientType);

            return ownedCount >= neededCount;
        }
    }
}