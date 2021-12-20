using Crawler.Crafting;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingManagerTests
    {
        [Test]
        public void Given_InventoryIsEmpty_When_TryCraft_Then_ReturnsCraftingResultFailed()
        {
            var craftingManager = new CraftingManager();
            
            var result = craftingManager.TryCraft();
            
            Assert.AreEqual(CraftingResult.Failed, result);
        }
    }
}