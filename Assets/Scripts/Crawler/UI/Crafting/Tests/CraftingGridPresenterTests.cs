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
            var models = new[]
            {
                new CraftingInventoryItem(0, 1, 0),
                new CraftingInventoryItem(1, 2, 0)
            };
            var inventory = new CraftingInventory(models);

            var viewItems = ConvertToViewModels(models);

            var viewModelConverter = Substitute.For<ICraftingInventoryItemViewModelConverter>();
            viewModelConverter
                .Convert(models)
                .Returns(viewItems);
            
            var presenter = new CraftingGridPresenter(_gridView, inventory, viewModelConverter);

            presenter.Present();
            
            _gridView
                .Received()
                .SpawnItems(Arg.Any<CraftingInventoryItemViewModel[]>());
        }

        private CraftingInventoryItemViewModel[] ConvertToViewModels(CraftingInventoryItem[] nodes)
        {
            return new CraftingInventoryItemViewModel[0];
        }

        [Test]
        public void When_PopulateGrid_Then_CraftingInventoryItemViewModelConverterConvertCalled()
        {
            var models = new[]
            {
                new CraftingInventoryItem(0, 1, 1),
                new CraftingInventoryItem(100, 10, 2),
                new CraftingInventoryItem(-1, -100, 1000),
            };
            var inventory = new CraftingInventory(models);

            var viewModelConverter = Substitute.For<ICraftingInventoryItemViewModelConverter>();
            
            var presenter = new CraftingGridPresenter(_gridView, inventory, viewModelConverter);
            
            presenter.Present();
            
            viewModelConverter
                .Received()
                .Convert(models);
        }

        [Test]
        public void When_Present_Then_CraftingGridViewInitializeCalled()
        {
            var viewModelConverter = new CraftingInventoryViewModelConverter();

            var inventoryItems = new[]
            {
                new CraftingInventoryItem(1,0,0)
            };
            var inventory = new CraftingInventory(inventoryItems);
            
            var presenter = new CraftingGridPresenter(_gridView, inventory, viewModelConverter);

            presenter.Present();
            
            _gridView
                .Received()
                .Initialize(5, 6);
        }
    }
}
