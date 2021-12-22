using Crawler.Crafting;
using DefaultNamespace;
using NUnit.Framework;

namespace Crawler.UI.Tests
{
    public class BootstrapperTests
    {
        [Test]
        public void When_Awake_Then_CraftingInventoryRegisteredToIocContainer()
        {
            var bootstrapper = new Bootstrapper();
            
            bootstrapper.Awake();
            
            Assert.DoesNotThrow(()=> IocContainer.GetSingleton<ICraftingInventory>());
        }
    }
}
