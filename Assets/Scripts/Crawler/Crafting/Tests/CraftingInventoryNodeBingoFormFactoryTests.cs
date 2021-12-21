using System.Collections.Generic;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingInventoryNodeBingoFormFactoryTests
    {
        private CraftingInventoryNodeBingoFormFactory _bingoFormFactory;
        private CraftingInventory _inventory;
        private CraftingFormation[] _formations;
        
        [SetUp]
        public void Setup()
        {
            _bingoFormFactory = new CraftingInventoryNodeBingoFormFactory();
            
            var inventoryNodes = new List<CraftingInventoryNode>
            {
                new CraftingInventoryNode(0),
                new CraftingInventoryNode(1),
                new CraftingInventoryNode(1),
                new CraftingInventoryNode(1),
                new CraftingInventoryNode(2),
                new CraftingInventoryNode(2),
            };
            _inventory = new CraftingInventory(inventoryNodes);
            
            var firstFormationNodes = new[]
            {
                new CraftingFormationNode(0),
                new CraftingFormationNode(1)
            };

            var secondFormationNodes = new[]
            {
                new CraftingFormationNode(2),
                new CraftingFormationNode(2)
            };

            var thirdFormationNodes = new[]
            {
                new CraftingFormationNode(1),
                new CraftingFormationNode(1)
            };

            _formations = new []
            {
                new CraftingFormation(firstFormationNodes), 
                new CraftingFormation(secondFormationNodes), 
                new CraftingFormation(thirdFormationNodes) 
            };
        }
        
        [Test]
        public void Given_EmptyCraftingInventory_When_Create_Then_ReturnsEmpty()
        {
            var emptyInventoryNodes = new List<CraftingInventoryNode>();
            var emptyInventory = new CraftingInventory(emptyInventoryNodes);
            
            var bingoForms = _bingoFormFactory.Create(emptyInventory, _formations);
            
            Assert.IsEmpty(bingoForms);
        }

        [Test]
        public void Given_EmptyFormations_When_Create_Then_ReturnsEmpty()
        {
            var emptyFormations = new CraftingFormation[0];

            var bingoForms = _bingoFormFactory.Create(_inventory, emptyFormations);
            
            Assert.IsEmpty(bingoForms);
        }
    }
}