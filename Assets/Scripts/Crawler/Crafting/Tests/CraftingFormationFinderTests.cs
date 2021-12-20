using NSubstitute;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingFormationFinderTests
    {
        [Test]
        public void Given_ValidArgs_When_Find_Then_ReturnsSuccessfulFindingResult()
        {
            var formationProvider = Substitute.For<ICraftingFormationProvider>();
            
            var formationFinder = new CraftingFormationFinder(formationProvider);
            
            var inventory = Substitute.For<ICraftingInventory>();
            inventory
                .HasItems
                .Returns(true);
            
            var result = formationFinder.Find(inventory);
            
            Assert.True(result.Success);
            Assert.IsNotEmpty(result.Formations);
        }

        [Test]
        public void Given_InventoryEmpty_When_Find_Then_ReturnsUnsuccessfulFindingResult()
        {
            var formationProvider = Substitute.For<ICraftingFormationProvider>();
            
            var formationFinder = new CraftingFormationFinder(formationProvider);

            var inventory = Substitute.For<ICraftingInventory>();
            inventory
                .HasItems
                .Returns(false);
            
            var result = formationFinder.Find(inventory);
            
            Assert.False(result.Success);
            Assert.IsEmpty(result.Formations);
        }
    }
}