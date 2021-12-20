using NSubstitute;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingFormationFinderTests
    {
        [Test]
        public void Given_ValidArgs_When_Find_Then_ReturnsSuccessfulFindingResult()
        {
            var formationFinder = new CraftingFormationFinder();
            
            var inventory = Substitute.For<ICraftingInventory>();
            inventory
                .HasItems
                .Returns(true);
            
            var result = formationFinder.Find(inventory);
            
            Assert.True(result.Success);
            Assert.IsNotEmpty(result.Formations);
        }
    }
}