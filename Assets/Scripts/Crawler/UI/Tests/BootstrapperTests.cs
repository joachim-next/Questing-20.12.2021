using Crawler.UI.Crafting;
using DefaultNamespace;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Crawler.UI.Tests
{
    public class BootstrapperTests
    {
        [Test]
        public void When_Start_CraftingGridPresenterPresentCalled()
        {
            var craftingGridPresenter = Substitute.For<ICraftingGridPresenter>();

            IocContainer.RegisterSingleton(craftingGridPresenter);
            
            var bootstrapper = new GameObject("Bootstrapper")
                .AddComponent<Bootstrapper>();
            
            bootstrapper.Start();

            craftingGridPresenter
                .Received()
                .Present();
        }

        [Test]
        public void When_Awake_Then_CraftingGridPresenterToIocContainer()
        {
            var bootstrapper = new GameObject("Bootstrapper")
                .AddComponent<Bootstrapper>();
            
            bootstrapper.Awake();
            
            Assert.DoesNotThrow(()=> IocContainer.GetSingleton<ICraftingGridPresenter>());
        }
    }
}
