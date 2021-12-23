using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class NotOwnedFormationFilterTests
    {
        private NotOwnedFormationFilter _formationFilter;
        private CraftingFormation[] _formations;
        private ICraftingInventory _inventory;
        
        [SetUp]
        public void Setup()
        {
            _formationFilter = new NotOwnedFormationFilter();
            
            var firstFormationNodes = new []
            {
                new CraftingFormationNode(0, 0, 0),
                new CraftingFormationNode(1, 0, 1)
            };
            var secondFormationNodes = new []
            {
                new CraftingFormationNode(1, 0, 1),
                new CraftingFormationNode(3, 0, 2)
            };
            var thirdFormationNodes = new []
            {
                new CraftingFormationNode(2, 1, 0),
                new CraftingFormationNode(2, 1, 1)
            };
            _formations = new []
            {
                new CraftingFormation(3, firstFormationNodes),
                new CraftingFormation(3, secondFormationNodes),
                new CraftingFormation(3, thirdFormationNodes)
            };
            
            var inventoryNodes = new []
            {
                new CraftingInventoryItem(0, 0, 0),
                new CraftingInventoryItem(1, 0, 1),
                new CraftingInventoryItem(3, 0, 2)
            };
            
            _inventory = new CraftingInventory(inventoryNodes);
        }
        
        [Test]
        public void Given_ValidArgs_When_Filter_Then_ReturnsFormationsWithOwnedIngredients()
        {
            var filteredFormations = _formationFilter.Execute(_formations, _inventory);
            
            var inventoryIngredientTypeToCountMap = new Dictionary<int, int>();

            foreach (var node in _inventory.Nodes)
            {
                var ingredientType = node.IngredientType;
                var count = _inventory.Nodes
                    .Count(x => x.IngredientType == ingredientType);

                inventoryIngredientTypeToCountMap[ingredientType] = count;
            }
            
            var formationIngredientTypeToCountMap = new Dictionary<int, int>();
            
            foreach (var formation in filteredFormations)
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
        
        [Test]
        public void Given_AnyIngredientMissing_When_Find_ReturnsFormationsThatDontNeedTheIngredients()
        {
            var filteredFormations = _formationFilter.Execute(_formations, _inventory);
            
            Assert.That(filteredFormations
                .All(y => y.Nodes
                    .All(z => _inventory.Nodes
                        .Any(i => i.IngredientType == z.IngredientType))));
        }
    }
}