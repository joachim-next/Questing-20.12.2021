using Crawler.Crafting;
using NSubstitute;
using NUnit.Framework;

namespace Crawler.UI.Crafting.Tests
{
    public class CraftingGridPresenterTests
    {
        private ICraftingGridView _gridView;
        
        [SetUp]
        public void Setup()
        {
            _gridView = Substitute.For<ICraftingGridView>();
        }
        
        [Test]
        public void When_PopulateGrid_Then_CraftingGridViewSpawnItemsCalled()
        {
            var inventoryNodes = new[]
            {
                new CraftingInventoryItem(0, 1, 0),
                new CraftingInventoryItem(1, 2, 0)
            };
            var inventory = new CraftingInventory(inventoryNodes);

            var presenter = new CraftingGridPresenter(_gridView, inventory);

            presenter.Present();
            
            var viewItems = ConvertToViewModels(inventoryNodes);
            
            _gridView
                .Received()
                .SpawnItems(Arg.Any<CraftingInventoryItemViewModel[]>());
        }

        private CraftingInventoryItemViewModel[] ConvertToViewModels(CraftingInventoryItem[] nodes)
        {
            return new CraftingInventoryItemViewModel[0];
        }
    }
}
