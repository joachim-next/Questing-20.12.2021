using System.Collections.Generic;
using System.Linq;

namespace Crawler.Crafting
{
    public class CraftingFormationFinder : ICraftingFormationFinder
    {
        private readonly ICraftingFormationProvider _formationProvider;

        public CraftingFormationFinder(ICraftingFormationProvider formationProvider)
        {
            _formationProvider = formationProvider;
        }
        
        public CraftingFormationFindingResult Find(ICraftingInventory inventory)
        {
            if(!inventory.HasItems)
                return new CraftingFormationFindingResult(false, new CraftingFormation[0]);

            var formations = _formationProvider.Provide();

            formations = RemoveFormationsWithNotOwnedIngredients(formations, inventory);
            
            return new CraftingFormationFindingResult(true, formations);
        }

        private CraftingFormation[] RemoveFormationsWithNotOwnedIngredients(CraftingFormation[] formations, 
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