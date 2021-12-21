using System.Collections.Generic;
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
            
            var inventoryNodes = new List<CraftingInventoryNode>()
            {    
                new CraftingInventoryNode(0, 0, 0),
                new CraftingInventoryNode(0, 0, 1)
            };
            var inventory = new CraftingInventory(inventoryNodes);
            
            var firstFormationNodes = new []
            {
                new CraftingFormationNode(0, 0, 0),
                new CraftingFormationNode(0, 0, 1)
            };
            var firstFormation = new CraftingFormation(firstFormationNodes);
            
            var secondFormationNodes = new []
            {
                new CraftingFormationNode(0, 0, 1),
                new CraftingFormationNode(0, 0, 2)
            };
            var secondFormation = new CraftingFormation(secondFormationNodes);

            var bingoForms = new[]
            {
                new CraftingInventoryNodeBingoForm(inventoryNodes[0], firstFormation),
                new CraftingInventoryNodeBingoForm(inventoryNodes[1], secondFormation),
            };
            
            var result = craftingInventoryNodeBingo.Execute(bingoForms, inventory);

            Assert.That(result.Formations.Contains(bingoForms[0].Formation));
            Assert.That(!result.Formations.Contains(bingoForms[1].Formation));
        }
    }
}