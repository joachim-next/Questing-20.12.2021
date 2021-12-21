using System.Collections.Generic;
using System.Linq;

namespace Crawler.Crafting
{
    public class CraftingInventoryNodeBingoFormFactory
    {
        public CraftingInventoryNodeBingoForm[] Create(CraftingInventory inventory, CraftingFormation[] formations)
        {
            var bingoForms = new List<CraftingInventoryNodeBingoForm>();

            foreach (var node in inventory.Nodes)
            {
                var validFormations = FormationsStartingWithIngredientType(node.IngredientType,
                    formations);

                var forms = CreateForms(node, validFormations);
                bingoForms.AddRange(forms);
            }

            return bingoForms.ToArray();
        }

        private CraftingFormation[] FormationsStartingWithIngredientType(int ingredientType, 
            CraftingFormation[] formations)
        {
            return formations
                .Where(x => x.Nodes[0].IngredientType == ingredientType)
                .ToArray();
        }

        private CraftingInventoryNodeBingoForm[] CreateForms(CraftingInventoryNode node, CraftingFormation[] formations)
        {
            var forms = new CraftingInventoryNodeBingoForm[formations.Length];

            for (int i = 0; i < formations.Length; i++)
            {
                var formation = formations[i];
                
                var form = new CraftingInventoryNodeBingoForm(node, formation);
                forms[i] = form;
            }

            return forms;
        }
    }
}