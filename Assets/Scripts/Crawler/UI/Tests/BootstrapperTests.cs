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
            var craftingContext = new CraftingContext();
            _bootstrapper = new Bootstrapper
            {
                CraftingContext = craftingContext
            };
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
