using NSubstitute;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingManagerTests
    {
        [Test]
        public void Given_PrerequisitesMet_When_TryCraft_Then_ReturnsCraftingResultSuccess()
        {
            var inventory = Substitute.For<ICraftingInventory>();
            inventory
                .HasItems
                .Returns(true);

            var formations = new CraftingFormation[1];
            
            var formationFinder = Substitute.For<ICraftingFormationFinder>();
            formationFinder
                .Find()
                .Returns(formations);

            var formationValidator = Substitute.For<ICraftingFormationValidator>();
            formationValidator
                .Validate(default)
                .ReturnsForAnyArgs(true);
            
            var craftingManager = new CraftingManager(inventory, formationFinder, formationValidator);
            
            var result = craftingManager.TryCraft();
            
            Assert.AreEqual(CraftingResult.Success, result);
        }
        
        [Test]
        public void Given_CraftingInventoryIsEmpty_When_TryCraft_Then_ReturnsCraftingResultFailed()
        {
            var inventory = Substitute.For<ICraftingInventory>();
            inventory
                .HasItems
                .Returns(false);
                
            var formations = new CraftingFormation[1];
            
            var formationFinder = Substitute.For<ICraftingFormationFinder>();
            formationFinder
                .Find()
                .Returns(formations);

            var formationValidator = Substitute.For<ICraftingFormationValidator>();
            formationValidator    
                .Validate(default)
                .ReturnsForAnyArgs(true);
            
            var craftingManager = new CraftingManager(inventory, formationFinder, formationValidator);
            
            var result = craftingManager.TryCraft();
            
            Assert.AreEqual(CraftingResult.Failed, result);
        }

        [Test]
        public void Given_NoFormationsFound_When_TryCraft_Then_ReturnsCraftingResultFailed()
        {
            var inventory = Substitute.For<ICraftingInventory>();
            inventory
                .HasItems
                .Returns(true);
            
            var formations = new CraftingFormation[0];
            
            var formationFinder = Substitute.For<ICraftingFormationFinder>();
            formationFinder
                .Find()
                .Returns(formations);
            
            var formationValidator = Substitute.For<ICraftingFormationValidator>();
            formationValidator
                .Validate(default)
                .ReturnsForAnyArgs(true);

            var craftingManager = new CraftingManager(inventory, formationFinder, formationValidator);
            
            var result = craftingManager.TryCraft();
            
            Assert.AreEqual(CraftingResult.Failed, result);
        }

        [Test]
        public void Given_FormationsNotValid_When_TryCraft_Then_ReturnsCraftingResultFailed()
        {
            var inventory = Substitute.For<ICraftingInventory>();
            inventory
                .HasItems
                .Returns(true);
                
            var formations = new CraftingFormation[1];
            
            var formationFinder = Substitute.For<ICraftingFormationFinder>();
            formationFinder
                .Find()
                .Returns(formations);

            var formationValidator = Substitute.For<ICraftingFormationValidator>();
            formationValidator    
                .Validate(default)
                .ReturnsForAnyArgs(false);
            
            var craftingManager = new CraftingManager(inventory, formationFinder, formationValidator);
            
            var result = craftingManager.TryCraft();
            
            Assert.AreEqual(CraftingResult.Failed, result);
        }
    }
}