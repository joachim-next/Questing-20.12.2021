using NSubstitute;
using NUnit.Framework;

namespace Crawler.UI.Crafting.Tests
{
    public class CraftingViewManagerTests
    {
        private ICraftingGridPresenter _gridPresenter;
        private ICraftingResultPresenter _resultPresenter;
        private CraftingViewManager _manager;
        
        [SetUp]
        public void Setup()
        {
            _gridPresenter = Substitute.For<ICraftingGridPresenter>();
            _resultPresenter = Substitute.For<ICraftingResultPresenter>();
            _manager = new CraftingViewManager(_gridPresenter, _resultPresenter);
        }
        
        [Test]
        public void When_Start_Then_ICraftingGridPresenterPresentCalled()
        {   
            _manager.Start();
            
            _gridPresenter
                .Received()
                .Present();
        }

        [Test]
        public void When_Start_Then_ICraftingResultPresenterPresentCalled()
        {   
            _manager.Start();
            
            _resultPresenter
                .Received()
                .Present();
        }
    }
}