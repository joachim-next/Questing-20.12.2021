using Crawler.Crafting;
using NSubstitute;
using NUnit.Framework;

namespace Crawler.UI.Crafting.Tests
{
    public class CraftingResultPresenterTests
    {
        private CraftingResultPresenter _presenter;
        private ICraftingResultView _view;
        private CraftingInventory _inventory;

        [SetUp]
        public void Setup()
        {   
            _view = Substitute.For<ICraftingResultView>();

            var inventoryItems = new[]
            {
                new CraftingInventoryItem(1, 1, 1),
                new CraftingInventoryItem(1, 2, 2)
            };
            _inventory = new CraftingInventory(inventoryItems);
            
            _presenter = new CraftingResultPresenter(_view, _inventory);
        }
        
        [Test]
        public void When_Present_Then_ICraftingResultViewShowResultCalled()
        {
            _presenter.Present();
            
            _view
                .Received()
                .ShowResult(_inventory);
        }
    }
}