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
            var formation = new CraftingFormation(3, formationNodes);
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

        [Test]
        public void Given_ValidItems_When_Resolve_Then_ReturnsContractWithCorrectIngredientType()
        {
            var inventoryItems = new[]
            {
                new CraftingInventoryItem(1, 2, 0),
                new CraftingInventoryItem(2, 2, 1),
            };
            var inventory = new CraftingInventory(inventoryItems);
           
            var formationNodes = new[]
            {
                new CraftingFormationNode(1, 0, 0),
                new CraftingFormationNode(2, 0, 1)
            };
            var formation = new CraftingFormation(3, formationNodes);
            var formations = new[] {formation};

            var form = new CraftingInventoryNodeBingoForm(inventoryItems[0], formation);
            form.TryCheck(inventoryItems[0]);
            form.TryCheck(inventoryItems[1]);
            var forms = new[]
            {
                form 
            };    
            
            var bingoFormFactory = Substitute.For<ICraftingInventoryNodeBingoFormFactory>();
            bingoFormFactory
                .Create(inventory, formations)
                .Returns(forms);

            var bingo = Substitute.For<ICraftingInventoryNodeBingo>();
            bingo
                .Execute(forms, inventory)
                .Returns(forms);
            
            var contractResolver = new CraftingContractResolver(inventory, bingoFormFactory, bingo);

            var contracts = contractResolver.Resolve(formations);
            
            var contract = contracts[0];
            
            Assert.AreEqual(formation.ResultItemIngredientType, 
                contract.ItemToBeCrafted.IngredientType);
        }
    }
}