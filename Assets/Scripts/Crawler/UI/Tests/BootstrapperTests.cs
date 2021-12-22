using Crawler.Crafting;
using DefaultNamespace;
using NUnit.Framework;

namespace Crawler.UI.Tests
{
    public class BootstrapperTests
    {
        private Bootstrapper _bootstrapper;

        [SetUp]
        public void Setup()
        {
            _bootstrapper = new Bootstrapper();
        }
        
        [Test]
        public void When_Awake_Then_CraftingInventoryRegisteredToIocContainer()
        {
            _bootstrapper.Awake();
            
            Assert.DoesNotThrow(()=> IocContainer.GetSingleton<ICraftingInventory>());
        }

        [Test]
        public void When_Awake_Then_ICraftingFormationFinderRegisteredToIocContainer()
        {
            _bootstrapper.Awake();
            
            Assert.DoesNotThrow(()=> IocContainer.GetSingleton<ICraftingFormationFinder>());
        }
    }
}
