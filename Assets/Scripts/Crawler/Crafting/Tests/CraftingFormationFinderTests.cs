using System.Collections.Generic;
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
        private CraftingFormation[] _filteredFormations;
        private ICraftingFormationFilter _formationFilter;
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

            _filteredFormations = new[]
            {
                _formations[0],
                _formations[1]
            };
            _formationFilter = Substitute.For<ICraftingFormationFilter>();
            _formationFilter
                .Execute(_formations, _inventory)
                .Returns(_filteredFormations);

            _bingoForms = new[]
            {
                new CraftingInventoryNodeBingoForm(inventoryNodes[0], _filteredFormations[0]),

                new CraftingInventoryNodeBingoForm(inventoryNodes[1], _filteredFormations[1]),
            };
            _bingoFormFactory = Substitute.For<ICraftingInventoryNodeBingoFormFactory>();
            _bingoFormFactory
                .Create(_inventory, _filteredFormations)
                .Returns(_bingoForms);

            var checkedFormations = new[]
            {
                _filteredFormations[0],
                _filteredFormations[1]
            };
            var bingoResult = new CraftingInventoryBingoResult(checkedFormations);
            _inventoryNodeBingo = Substitute.For<ICraftingInventoryNodeBingo>();
            _inventoryNodeBingo
                .Execute(_bingoForms, _inventory)
                .Returns(bingoResult);

            _formationFinder = new CraftingFormationFinder(_formationProvider, _formationFilter, _bingoFormFactory, 
                _inventoryNodeBingo);
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
        public void Given_ValidArgs_When_Find_Then_ICraftingFormationFilterFilterCalled()
        {
            _formationFinder.Find(_inventory);

            _formationFilter
                .Received()
                .Execute(_formations, _inventory);
        }
        
        [Test]
        public void Given_ValidArgs_When_Find_Then_ICraftingInventoryNodeBingoFormFactoryCreateCalled()
        {
            _formationFinder.Find(_inventory);
            
            _bingoFormFactory
                .Received()
                .Create(_inventory, _filteredFormations);
        }

        [Test]
        public void Given_ValidArgs_When_Find_Then_ICraftingInventoryNodeBingoExecuteCalled()
        {
            _formationFinder.Find(_inventory);
            
            _inventoryNodeBingo
                .Received()
                .Execute(_bingoForms, _inventory);
        }
    }
}