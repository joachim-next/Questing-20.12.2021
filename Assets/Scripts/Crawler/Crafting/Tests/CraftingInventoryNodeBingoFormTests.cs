using System.Linq;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingInventoryNodeBingoFormTests
    {
        [Test]
        public void Given_TryCheckCalledWithValidArgs_When_TryCheck_Then_NodesChecked()
        {
            var item = new CraftingInventoryItem(1, 1, 1);

            var formationNodes = new[]
            {
                new CraftingFormationNode(1, 0, 0),
                new CraftingFormationNode(2, 0, 1)
            };
            var formation = new CraftingFormation(formationNodes);
            
            var form = new CraftingInventoryNodeBingoForm(item, formation);

            form.TryCheck(1, 1, 1);
            form.TryCheck(2, 1, 2);

            Assert.That(form.CheckedNodes.All(x => x));
        }

        [Test]
        public void Given_InvalidX_When_TryCheck_Then_NodesNotChecked()
        {
            var item = new CraftingInventoryItem(1, 2, 1);
            
            var formationNodes = new []
            {
                new CraftingFormationNode(1, 0, 0) 
            };
            var formation = new CraftingFormation(formationNodes);
            
            var form = new CraftingInventoryNodeBingoForm(item, formation);

            var invalidX = item.X + 1;
            form.TryCheck(item.IngredientType, invalidX, item.Y);
            
            Assert.That(form.CheckedNodes.All(x => !x));
        }

        [Test]
        public void Given_InvalidY_When_TryCheck_Then_NodesNotChecked()
        {
            var item = new CraftingInventoryItem(1, 3, 4);
            
            var formationNodes = new[]
            {
                new CraftingFormationNode(1, 0, 0) 
            };
            var formation = new CraftingFormation(formationNodes);
            
            var form = new CraftingInventoryNodeBingoForm(item, formation);

            var invalidY = item.Y + 1;
            form.TryCheck(item.IngredientType, item.X, invalidY);
            
            Assert.That(form.CheckedNodes.All(x => !x));
        }

        [Test]
        public void Given_InvalidIngredientType_When_TryCheck_Then_NodesNotChecked()
        {
            var item = new CraftingInventoryItem(1, 5, 3);
            
            var formationNodes = new[]
            {
                new CraftingFormationNode(1, 0, 0)
            };
            var formation = new CraftingFormation(formationNodes);
            
            var form = new CraftingInventoryNodeBingoForm(item, formation);

            var invalidIngredientType = item.IngredientType + 1;
            form.TryCheck(invalidIngredientType, item.X, item.Y);
            
            Assert.That(form.CheckedNodes.All(x => !x));
        }
    }
}