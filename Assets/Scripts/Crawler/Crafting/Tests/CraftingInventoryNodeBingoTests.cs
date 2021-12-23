using System.Linq;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingInventoryNodeBingoTests
    {
        [Test]
        public void Given_ValidArgs_When_Execute_Then_CorrectFormsChecked()
        {
            var craftingInventoryNodeBingo = new CraftingInventoryNodeBingo();
            
            var inventoryNodes = new []
            {    
                new CraftingInventoryItem(0, 0, 0),
                new CraftingInventoryItem(0, 0, 1)
            };
            var inventory = new CraftingInventory(inventoryNodes);
            
            var firstFormationNodes = new []
            {
                new CraftingFormationNode(0, 0, 0),
                new CraftingFormationNode(0, 0, 1)
            };
            var firstFormation = new CraftingFormation(3, firstFormationNodes);
            
            var secondFormationNodes = new []
            {
                new CraftingFormationNode(0, 0, 1),
                new CraftingFormationNode(0, 0, 2)
            };
            var secondFormation = new CraftingFormation(3, secondFormationNodes);

            var bingoForms = new[]
            {
                new CraftingInventoryNodeBingoForm(inventoryNodes[0], firstFormation),
                new CraftingInventoryNodeBingoForm(inventoryNodes[1], secondFormation),
            };
            
            var executedForms = craftingInventoryNodeBingo.Execute(bingoForms, inventory);

            Assert.That(executedForms[0].CheckedNodes.All(x=> x));
            Assert.That(executedForms[1].CheckedNodes.All(x=> !x));
        }
    }
}