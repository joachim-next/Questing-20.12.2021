using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingFormationFinderTests
    {
        private ICraftingFormationProvider _formationProvider;
        private ICraftingInventoryNodeBingoFormFactory _bingoFormFactory;
        private ICraftingInventory _inventory;
        private CraftingFormationFinder _formationFinder;
        
        [SetUp]
        public void SetUp()
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
            _formationProvider = Substitute.For<ICraftingFormationProvider>();
            _formationProvider
                .Provide()
                .Returns(formations);

            _bingoFormFactory = Substitute.For<ICraftingInventoryNodeBingoFormFactory>();
            
            _formationFinder = new CraftingFormationFinder(_formationProvider, _bingoFormFactory);
            
            var ingredients = new List<CraftingInventoryNode>
            {
                new CraftingInventoryNode(0),
                new CraftingInventoryNode(1),
                new CraftingInventoryNode(3)
            };
            
            _inventory = new CraftingInventory(ingredients);
        }
        
        [Test]
        public void Given_ValidArgs_When_Find_Then_ReturnsSuccessfulFindingResult()
        {
            var result = _formationFinder.Find(_inventory);
            
            Assert.True(result.Success);
            Assert.IsNotEmpty(result.Formations);
        }

        [Test]
        public void Given_InventoryEmpty_When_Find_Then_ReturnsUnsuccessfulFindingResult()
        {
            var emptyInventory = new CraftingInventory(new List<CraftingInventoryNode>());

            var result = _formationFinder.Find(emptyInventory);
            
            Assert.False(result.Success);
            Assert.IsEmpty(result.Formations);
        }

        [Test]
        public void Given_AnyIngredientMissing_When_Find_ReturnsFormationsThatDontNeedTheIngredients()
        {
            var result = _formationFinder.Find(_inventory);
            
            Assert.That(result.Formations
                .All(y => y.Nodes
                    .All(z => _inventory.Nodes
                        .Any(i => i.IngredientType == z.IngredientType))));
        }

        [Test]
        public void Given_TooLittleIngredientsOwned_When_Find_Then_ReturnsFormationsWithOnlyOwnedIngredients()
        {
            var result = _formationFinder.Find(_inventory);
            
            var inventoryIngredientTypeToCountMap = new Dictionary<int, int>();

            foreach (var node in _inventory.Nodes)
            {
                var ingredientType = node.IngredientType;
                var count = _inventory.Nodes
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

        [Test]
        public void Given_ValidArgs_When_Find_Then_ICraftingInventoryNodeBingoFormFactoryCreateCalled()
        {
            _formationFinder.Find(_inventory);
            
            _bingoFormFactory
                .Received()
                .Create(_inventory, Arg.Any<CraftingFormation[]>());
        }
    }
}