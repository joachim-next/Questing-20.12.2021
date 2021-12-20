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
                if(IngredientForFormationExist(formations[i], inventory))
                    continue;
                indices.Add(i);
            }

            return indices.ToArray();
        }

        private bool IngredientForFormationExist(CraftingFormation formation, ICraftingInventory inventory)
        {
            return formation.Nodes
                .All(x=> inventory.Nodes
                    .Any(y => y.IngredientType == x.IngredientType));
        }
    }
}