using System.Collections.Generic;
using System.Linq;

namespace Crawler.Crafting
{
    public class CraftingFormationFinder : ICraftingFormationFinder
    {
        private readonly ICraftingFormationProvider _formationProvider;
        private readonly ICraftingInventoryNodeBingoFormFactory _bingoFormFactory;
        private readonly ICraftingInventoryNodeBingo _inventoryNodeBingo;

        public CraftingFormationFinder(ICraftingFormationProvider formationProvider, 
            ICraftingInventoryNodeBingoFormFactory bingoFormFactory, ICraftingInventoryNodeBingo inventoryNodeBingo)
        {
            _formationProvider = formationProvider;
            _bingoFormFactory = bingoFormFactory;
            _inventoryNodeBingo = inventoryNodeBingo;
        }
        
        // 1. Clear formations you don't have items for
        //    - Any ingredient missing
        //    - Too little of any ingredient
        // 2. Create bingo-forms for each possible formation from each node
        // 3. Do bingo
        // 4. Clear every formation that didn't get full bingo
        // 5. Return remaining.
        public CraftingFormationFindingResult Find(ICraftingInventory inventory)
        {
            if(!inventory.HasItems)
                return new CraftingFormationFindingResult(false, new CraftingFormation[0]);

            var formations = _formationProvider.Provide();

            formations = RemoveFormationsWithNotOwnedIngredients(formations, inventory);

            var bingoForms = _bingoFormFactory.Create(inventory, formations);

            var bingoResults = _inventoryNodeBingo.Execute(bingoForms, inventory);
            
            return new CraftingFormationFindingResult(true, bingoResults.Formations);
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