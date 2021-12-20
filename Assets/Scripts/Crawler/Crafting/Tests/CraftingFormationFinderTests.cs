using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingFormationFinderTests
    {
        [Test]
        public void Given_ValidArgs_When_Find_Then_ReturnsSuccessfulFindingResult()
        {
            var firstFormationNodes = new []
            {
                new CraftingFormationNode(0),
                new CraftingFormationNode(1)
            };
            var secondFormationNodes = new []
            {
                new CraftingFormationNode(1),
                new CraftingFormationNode(3)
            };
            var thirdFormationNodes = new []
            {
                new CraftingFormationNode(2),
                new CraftingFormationNode(2)
            };
            var formations = new []
            {
                new CraftingFormation(firstFormationNodes),
                new CraftingFormation(secondFormationNodes),
                new CraftingFormation(thirdFormationNodes)
            };
            var formationProvider = Substitute.For<ICraftingFormationProvider>();
            formationProvider
                .Provide()
                .Returns(formations);
            
            var formationFinder = new CraftingFormationFinder(formationProvider);
            
            var ingredients = new List<CraftingInventoryNode>
            {
                new CraftingInventoryNode(0),
                new CraftingInventoryNode(1),
                new CraftingInventoryNode(3)
            };
            var inventory = new CraftingInventory(ingredients);
            
            var result = formationFinder.Find(inventory);
            
            Assert.True(result.Success);
            Assert.IsNotEmpty(result.Formations);
        }

        [Test]
        public void Given_InventoryEmpty_When_Find_Then_ReturnsUnsuccessfulFindingResult()
        {
            var formationProvider = Substitute.For<ICraftingFormationProvider>();
            
            var formationFinder = new CraftingFormationFinder(formationProvider);

            var inventory = new CraftingInventory(new List<CraftingInventoryNode>());

            var result = formationFinder.Find(inventory);
            
            Assert.False(result.Success);
            Assert.IsEmpty(result.Formations);
        }

        [Test]
        public void Given_AnyIngredientMissing_When_Find_ReturnsFormationsThatDontNeedTheIngredients()
        {
            var firstFormationNodes = new []
            {
                new CraftingFormationNode(0),
                new CraftingFormationNode(1)
            };
            var secondFormationNodes = new []
            {
                new CraftingFormationNode(1),
                new CraftingFormationNode(3)
            };
            var thirdFormationNodes = new []
            {
                new CraftingFormationNode(2),
                new CraftingFormationNode(2)
            };
            var formations = new []
            {
                new CraftingFormation(firstFormationNodes),
                new CraftingFormation(secondFormationNodes),
                new CraftingFormation(thirdFormationNodes)
            };

            var formationProvider = Substitute.For<ICraftingFormationProvider>();
            formationProvider
                .Provide()
                .Returns(formations);
            
            var formationFinder = new CraftingFormationFinder(formationProvider);
            
            var ingredients = new List<CraftingInventoryNode>
            {
                new CraftingInventoryNode(0),
                new CraftingInventoryNode(1),
                new CraftingInventoryNode(3)
            };
            var inventory = new CraftingInventory(ingredients);
            
            var result = formationFinder.Find(inventory);
            
            Assert.That(result.Formations.All(
                y => y.Nodes.All(
                    z => inventory.Nodes.Any(
                        i => i.IngredientType == z.IngredientType))));
        }

        [Test]
        public void Given_TooLittleIngredientsOwned_When_Find_Then_ReturnsFormationsWithOnlyOwnedIngredients()
        {
            var firstFormationNodes = new []
            {
                new CraftingFormationNode(0),
                new CraftingFormationNode(1)
            };
            var secondFormationNodes = new []
            {
                new CraftingFormationNode(1),
                new CraftingFormationNode(3)
            };
            var thirdFormationNodes = new []
            {
                new CraftingFormationNode(2),
                new CraftingFormationNode(2)
            };
            var formations = new []
            {
                new CraftingFormation(firstFormationNodes),
                new CraftingFormation(secondFormationNodes),
                new CraftingFormation(thirdFormationNodes)
            };

            var formationProvider = Substitute.For<ICraftingFormationProvider>();
            formationProvider
                .Provide()
                .Returns(formations);

            var formationFinder = new CraftingFormationFinder(formationProvider);
            
            var ingredients = new List<CraftingInventoryNode>
            {
                new CraftingInventoryNode(0),
                new CraftingInventoryNode(1),
                new CraftingInventoryNode(3)
            };
            var inventory = new CraftingInventory(ingredients);

            var result = formationFinder.Find(inventory);
            
            var inventoryIngredientTypeToCountMap = new Dictionary<int, int>();

            foreach (var node in inventory.Nodes)
            {
                var ingredientType = node.IngredientType;
                var count = inventory.Nodes
                    .Count(x => x.IngredientType == ingredientType);

                inventoryIngredientTypeToCountMap[ingredientType] = count;
            }
            
            var formationIngredientTypeToCountMap = new Dictionary<int, int>();
            
            foreach (var formation in result.Formations)
            {
                foreach (var node in formation.Nodes)
                {
                    var ingredientType = node.IngredientType;
                    var count = formation.Nodes
                        .Count(x => x.IngredientType == ingredientType);
                    
                    formationIngredientTypeToCountMap[ingredientType] = count;
                }
            }

            foreach (var group in formationIngredientTypeToCountMap)
            {   
                Assert.GreaterOrEqual(inventoryIngredientTypeToCountMap[group.Key], group.Value);
            }
        }
    }
}