using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void Given_ValidArgs_When_Create_Then_ReturnsBingoForms()
        {
            var bingoForms = _bingoFormFactory.Create(_inventory, _formations);
            
            AssertCorrectFormsCreated(bingoForms, _inventory, _formations);
        }

        private void AssertCorrectFormsCreated(CraftingInventoryNodeBingoForm[] actualBingoForms, 
            CraftingInventory inventory, CraftingFormation[] formations)
        {
            foreach (var node in inventory.Nodes)
            {
                var bingoForms = CreateBingoForms(node, formations);

                foreach (var form in bingoForms)
                {
                    for (int i = 0; i < actualBingoForms.Length; i++)
                    {
                        if (actualBingoForms[i] == null ||
                            !actualBingoForms[i].Equals(form))
                        {
                            continue;
                        }

                        actualBingoForms[i] = null;
                    }
                }
            }

            Assert.That(actualBingoForms.All(x => x == null));
        }

        private CraftingInventoryNodeBingoForm[] CreateBingoForms(CraftingInventoryNode node, 
            CraftingFormation[] formations)
        {
            var forms = new List<CraftingInventoryNodeBingoForm>();
            
            foreach (var formation in formations)
            {
                var bingoForm = new CraftingInventoryNodeBingoForm(node, formation);
                forms.Add(bingoForm);
            }

            return forms.ToArray();
        }
    }
}