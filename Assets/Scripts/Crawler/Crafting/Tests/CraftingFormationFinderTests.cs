using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingFormationFinderTests
    {
        private CraftingFormation[] _formations;
        private ICraftingFormationProvider _formationProvider;
        private CraftingInventoryNodeBingoForm[] _bingoForms;
        private ICraftingInventoryNodeBingoFormFactory _bingoFormFactory;
        private ICraftingInventory _inventory;
        private CraftingInventoryBingoResult _bingoResult;
        private ICraftingInventoryNodeBingo _inventoryNodeBingo;
        private CraftingFormationFinder _formationFinder;
        
        [SetUp]
        public void SetUp()
        {
            var inventoryNodes = new List<CraftingInventoryNode>
            {
                new CraftingInventoryNode(0, 0, 0),
                new CraftingInventoryNode(1, 0, 1),
                new CraftingInventoryNode(3, 0, 2)
            };
            
            _inventory = new CraftingInventory(inventoryNodes);
            
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
                new CraftingFormation(firstFormationNodes),
                new CraftingFormation(secondFormationNodes),
                new CraftingFormation(thirdFormationNodes)
            };
            _formationProvider = Substitute.For<ICraftingFormationProvider>();
            _formationProvider
                .Provide()
                .Returns(_formations);

            _bingoForms = new[]
            {
                new CraftingInventoryNodeBingoForm(inventoryNodes[0], _formations[0]),

                new CraftingInventoryNodeBingoForm(inventoryNodes[1], _formations[1]),
            };
            _bingoFormFactory = Substitute.For<ICraftingInventoryNodeBingoFormFactory>();
            _bingoFormFactory
                .Create(_inventory, _formations)
                .Returns(_bingoForms);

            var checkedFormations = new[]
            {
                _formations[0],
                _formations[1]
            };
            var bingoResult = new CraftingInventoryBingoResult(checkedFormations);
            _inventoryNodeBingo = Substitute.For<ICraftingInventoryNodeBingo>();
            _inventoryNodeBingo
                .Execute(Arg.Any<CraftingInventoryNodeBingoForm[]>(), _inventory)
                .Returns(bingoResult);

            _formationFinder = new CraftingFormationFinder(_formationProvider, _bingoFormFactory, _inventoryNodeBingo);
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

        [Test]
        public void Given_ValidArgs_When_Find_Then_ICraftingInventoryNodeBingoExecuteCalled()
        {
            _formationFinder.Find(_inventory);
            
            _inventoryNodeBingo
                .Received()
                .Execute(Arg.Any<CraftingInventoryNodeBingoForm[]>(), _inventory);
        }
    }
}