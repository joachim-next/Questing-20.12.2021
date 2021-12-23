using Crawler.Crafting;
using UnityEngine;

namespace Crawler.UI
{
    [CreateAssetMenu(fileName = nameof(UnityCraftingFormationProvider), menuName = nameof(UnityCraftingFormationProvider))]
    public class UnityCraftingFormationProvider : ScriptableObject, ICraftingFormationProvider
    {
        public UnityCraftingFormation[] Formations;

        public CraftingFormation[] Provide()
        {
            return Convert(Formations);
        }

        private CraftingFormation[] Convert(UnityCraftingFormation[] targetFormations)
        {
            var formations = new CraftingFormation[targetFormations.Length];

            for(int i = 0; i < targetFormations.Length; i++)
            {
                var targetFormation = targetFormations[i];
                var nodes = ConvertNodes(targetFormation.Nodes);
                
                formations[i] = new CraftingFormation(targetFormation.ItemToBeCraftedIngredientType, nodes);
            }
            
            return formations;
        }

        private CraftingFormationNode[] ConvertNodes(UnityCraftingFormationNode[] targetNodes)
        {
            var nodes = new CraftingFormationNode[targetNodes.Length];

            for (int i = 0; i < targetNodes.Length; i++)
            {
                var targetNode = targetNodes[i];
                
                nodes[i] = new CraftingFormationNode(targetNode.IngredientType, targetNode.RelativeX, 
                    targetNode.RelativeY);
            }
            
            return nodes;
        }
    }
}
