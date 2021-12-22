using NSubstitute;
using NUnit.Framework;

namespace Crawler.UI.Crafting.Tests
{
    public class CraftingViewManagerTests
    {
        [Test]
        public void When_Start_Then_ICraftingGridPresenterPresentCalled()
        {
            var gridPresenter = Substitute.For<ICraftingGridPresenter>();
            
            var manager = new CraftingViewManager(gridPresenter);
            
            manager.Start();
            
            gridPresenter
                .Received()
                .Present();
        }
    }
}