using NSubstitute;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingFormationFinderTests
    {
        private CraftingFormation[] _formations;
        private ICraftingFormationProvider _formationProvider;
        private ICraftingInventory _inventory;
        private CraftingFormation[] _filteredFormations;
        private ICraftingFormationFilter _formationFilter;
        private CraftingFormationFinder _formationFinder;
        
        [SetUp]
        public void SetUp()
        {
            var inventoryNodes = new []
            {
                new CraftingInventoryItem(0, 0, 0),
                new CraftingInventoryItem(1, 0, 1),
                new CraftingInventoryItem(3, 0, 2)
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

            _formationFinder = new CraftingFormationFinder(_formationProvider, _formationFilter);
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
            var emptyInventory = new CraftingInventory(new CraftingInventoryItem[0]);

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
    }
}