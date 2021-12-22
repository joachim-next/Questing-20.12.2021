using Crawler.Crafting;
using DefaultNamespace;
using NSubstitute;
using NUnit.Framework;

namespace Crawler.UI.Crafting.Tests
{
    public class CraftingGridPresenterTests
    {
        private ICraftingGridView _gridView;
        private ICraftingResultView _resultView;
        
        [SetUp]
        public void Setup()
        {
            _gridView = Substitute.For<ICraftingGridView>();
            _resultView = Substitute.For<ICraftingResultView>();
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
            
            var presenter = new CraftingGridPresenter(_gridView, _resultView, inventory, viewModelConverter);

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
            
            var presenter = new CraftingGridPresenter(_gridView, _resultView, inventory, viewModelConverter);
            
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
            
            var presenter = new CraftingGridPresenter(_gridView, _resultView, inventory, viewModelConverter);

            presenter.Present();
            
            _gridView
                .Received()
                .Initialize(5, 6);
        }
        
        [Test]
        public void When_Present_Then_ICraftingResultViewShowResult()
        {
            var viewModelConverter = new CraftingInventoryViewModelConverter();
            
            var inventoryItems = new[]
            {
                new CraftingInventoryItem(0, 2, 0),
                new CraftingInventoryItem(1, 2, 1)
            };
            var inventory = new CraftingInventory(inventoryItems);
            
            var presenter = new CraftingGridPresenter(_gridView, _resultView, inventory, viewModelConverter);

            presenter.Present();
            
            _resultView
                .Received()
                .ShowResult(inventory);
        }

        [Test]
        public void When_UpdateModel_Then_OnModelChangedFired()
        {
            bool eventFired = false;

            var viewModelConverter = new CraftingInventoryViewModelConverter();

            var inventoryItems = new[]
            {
                new CraftingInventoryItem(1, 1, 1) 
            };
            var inventory = new CraftingInventory(inventoryItems);
            
            var presenter = new CraftingGridPresenter(_gridView, _resultView, inventory, viewModelConverter);
            
            presenter.OnModelChanged += () => { eventFired = true; };
            
            presenter.UpdateModel(new CraftingInventoryItemViewModel[0]);
            
            Assert.True(eventFired);
        }

        [Test]
        public void When_UpdateModel_Then_ModelChanged()
        {
            var inventoryItems = new[]
            {
                new CraftingInventoryItem(1, 1, 1) 
            };
            var inventory = new CraftingInventory(inventoryItems);
            
            var viewModelConverter = new CraftingInventoryViewModelConverter();
            
            var presenter = new CraftingGridPresenter(_gridView, _resultView, inventory, viewModelConverter);

            var updatedViewModels = new[]
            {
                new CraftingInventoryItemViewModel(1, 0, 0) 
            };
            presenter.UpdateModel(updatedViewModels);

            var actual = IocContainer.GetSingleton<ICraftingInventory>();
            
            var expectedInventoryItems = new[]
            {
                new CraftingInventoryItem(1, 0, 0), 
            };
            var expected = new CraftingInventory(expectedInventoryItems);
            
            Assert.AreEqual(expected, actual);
        }
    }
}
