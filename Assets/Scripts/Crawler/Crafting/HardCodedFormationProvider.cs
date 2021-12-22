namespace Crawler.Crafting
{
    public class HardCodedFormationProvider : ICraftingFormationProvider
    {
        public CraftingFormation[] Provide()
        {
            var formationNodes = new[]
            {
                new CraftingFormationNode(2, 0, 0),
                new CraftingFormationNode(2, 1, 0),
                new CraftingFormationNode(2, 2, 0),
                new CraftingFormationNode(2, 1, -1),
                new CraftingFormationNode(2, 1, -2)
            };
            
            return new[]
            {
                new CraftingFormation(formationNodes) 
            };
        }
    }
}