using System;
using Crawler.Crafting;
using NUnit.Framework;

namespace Crawler.UI.Crafting.Tests
{
    public class CraftingInventoryViewModelConverterTests
    {
        private CraftingInventoryViewModelConverter _converter;
        
        [SetUp]
        public void Setup()
        {
            _converter = new CraftingInventoryViewModelConverter();
        }
        
        [Test]
        public void Given_EmptyModels_When_Convert_Then_Throws()
        {
            var emptyModels = new CraftingInventoryItem[0];
            Assert.Throws<ArgumentException>(()=> _converter.Convert(emptyModels));
        }

        [Test]
        public void Given_NullModels_When_Convert_Then_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _converter.Convert(null));
        }
    }
}