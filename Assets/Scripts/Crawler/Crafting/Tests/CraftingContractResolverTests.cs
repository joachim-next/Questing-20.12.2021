using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingContractResolverTests
    {
        private ICraftingInventory _inventory;
        private CraftingFormation[] _formations;
        private ICraftingInventoryNodeBingoFormFactory _bingoFormFactory;
        private ICraftingInventoryNodeBingo _inventoryNodeBingo;
        private CraftingContractResolver _contractResolver;
        
        [SetUp]
        public void Setup()
        {
            var item = new CraftingInventoryItem(1, 0, 0);
            var inventoryItems = new[]
            {
                item  
            };
            _inventory = new CraftingInventory(inventoryItems);
            
            var formationNodes = new[]
            {
                new CraftingFormationNode(1, 0, 0)
            };
            var formation = new CraftingFormation(formationNodes);
            _formations = new[] { formation };

            var form = new CraftingInventoryNodeBingoForm(item, formation);
            form.TryCheck(item);
            var bingoForms = new []
            {
                form
            };

            _bingoFormFactory = Substitute.For<ICraftingInventoryNodeBingoFormFactory>();
            _bingoFormFactory
                .Create(_inventory, _formations)
                .Returns(bingoForms);

            _inventoryNodeBingo = Substitute.For<ICraftingInventoryNodeBingo>(); 
            _inventoryNodeBingo
                .Execute(bingoForms, _inventory)
                .Returns(bingoForms);
            
            _contractResolver = new CraftingContractResolver(_inventory, _bingoFormFactory, _inventoryNodeBingo);
        }
        
        [Test]
        public void Given_ValidArgs_When_Resolve_Then_ReturnsContractsWithItems()
        {
            var contracts = _contractResolver.Resolve(_formations);

            var validItems = _inventory.Nodes
                .Take(5)
                .ToArray();

            var validatedFormation = contracts.First();
            Assert.AreEqual(validItems.Length, validatedFormation.ItemsToBeUsed.Length);

            foreach (var item in validItems)
            {
                Assert.That(validatedFormation.ItemsToBeUsed.Contains(item));
            }
        }
        
        [Test]
        public void Given_ValidArgs_When_Find_Then_ICraftingInventoryNodeBingoFormFactoryCreateCalled()
        {   
            _contractResolver.Resolve(_formations);
            
            _bingoFormFactory
                .Received()
                .Create(_inventory, _formations);
        }

        [Test]
        public void Given_ValidArgs_When_Find_Then_ICraftingInventoryNodeBingoExecuteCalled()
        {
            _contractResolver.Resolve(_formations);
            
            _inventoryNodeBingo
                .Received()
                .Execute(_bingoFormFactory.Create(_inventory, _formations), _inventory);
        }

        [Test]
        public void Given_EmptyArray_When_Resolve_Throws()
        {
            Assert.Throws<ArgumentException>(() => _contractResolver.Resolve(new CraftingFormation[0]));
        }

        [Test]
        public void Given_NullArray_When_Resolve_Throws()
        {
            Assert.Throws<ArgumentNullException>(()=> _contractResolver.Resolve(null));
        }

        [Test]
        public void Given_ArrayWithNullEntries_When_Resolve_Throws()
        {
            Assert.Throws<ArgumentException>(()=> _contractResolver.Resolve(new CraftingFormation[5]));
        }
    }
}