﻿using NSubstitute;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingManagerTests
    {
        [Test]
        public void Given_PrerequisitesMet_When_TryCraft_Then_ReturnsCraftingResultSuccess()
        {
            var inventory = new CraftingInventory();

            var formationValidator = Substitute.For<ICraftingFormationValidator>();
            formationValidator
                .FindFormations()
                .Returns(true);
            
            var craftingManager = new CraftingManager(inventory, formationValidator);
            
            var result = craftingManager.TryCraft();
            
            Assert.AreEqual(CraftingResult.Success, result);
        }
        
        [Test]
        public void Given_CraftingInventoryIsEmpty_When_TryCraft_Then_ReturnsCraftingResultFailed()
        {
            var inventory = new CraftingInventory();
            var formationValidator = Substitute.For<ICraftingFormationValidator>();

            formationValidator
                .FindFormations()
                .Returns(true);
            
            var craftingManager = new CraftingManager(inventory, formationValidator);
            
            var result = craftingManager.TryCraft();
            
            Assert.AreEqual(CraftingResult.Failed, result);
        }

        [Test]
        public void Given_NoFormationsFound_When_TryCraft_Then_ReturnsCraftingResultFailed()
        {
            var inventory = new CraftingInventory();
            var formationValidator = Substitute.For<ICraftingFormationValidator>();

            formationValidator
                .FindFormations()
                .Returns(false);

            var craftingManager = new CraftingManager(inventory, formationValidator);
            
            var result = craftingManager.TryCraft();
            
            Assert.AreEqual(CraftingResult.Failed, result);
        }
    }
}