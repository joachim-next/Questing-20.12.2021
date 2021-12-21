using System;
using System.Linq;
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

        [Test]
        public void Given_ValidArgs_When_Convert_Then_ReturnsViewModelsForEachModel()
        {
            var models = new []
            {
                new CraftingInventoryItem(0, 0, 0),
                new CraftingInventoryItem(1, 1, 1),
                new CraftingInventoryItem(2, 2, 2)
            };
            
            var viewModels = _converter.Convert(models);

            foreach (var model in models)
            {
                Assert.That(viewModels.Count(x => 
                    x.IngredientType == model.IngredientType && 
                    x.X == model.X && 
                    x.Y == model.Y) == 1);
            }
        }
    }
}