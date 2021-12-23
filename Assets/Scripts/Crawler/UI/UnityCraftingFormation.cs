using System;

namespace Crawler.UI
{
    [Serializable]
    public class UnityCraftingFormation
    {
        public int ItemToBeCraftedIngredientType;
        public UnityCraftingFormationNode[] Nodes;
    }
}