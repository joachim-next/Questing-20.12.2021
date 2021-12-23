using System.Linq;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingInventoryNodeBingoFormTests
    {
        [Test]
        public void Given_TryCheckCalledWithValidArgs_When_TryCheck_Then_NodesChecked()
        {
            var item1 = new CraftingInventoryItem(1, 1, 1);
            var item2 = new CraftingInventoryItem(2, 0, 1);
            
            var formationNodes = new[]
            {
                new CraftingFormationNode(1, 0, 0),
                new CraftingFormationNode(2, -1, 0)
            };
            var formation = new CraftingFormation(formationNodes);
            
            var form = new CraftingInventoryNodeBingoForm(item1, formation);

            form.TryCheck(item1);
            form.TryCheck(item2);

            Assert.That(form.CheckedNodes.All(x => x));
        }

        [Test]
        public void Given_InvalidItem_When_TryCheck_Then_NodesNotChecked()
        {
            var item = new CraftingInventoryItem(1, 2, 1);
            
            var formationNodes = new []
            {
                new CraftingFormationNode(1, 0, 0) 
            };
            var formation = new CraftingFormation(formationNodes);
            
            var form = new CraftingInventoryNodeBingoForm(item, formation);

            var invalidItem = new CraftingInventoryItem(0, 0, 0);
            form.TryCheck(invalidItem);
            
            Assert.That(form.CheckedNodes.All(x => !x));
        }
    }
}