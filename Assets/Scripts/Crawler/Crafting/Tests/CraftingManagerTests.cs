﻿using NSubstitute;
using NUnit.Framework;

namespace Crawler.Crafting.Tests
{
    public class CraftingManagerTests
    {
        [Test]
        public void Given_PrerequisitesMet_When_TryGetContracts_Then_ReturnsContracts()
        {
            var inventory = Substitute.For<ICraftingInventory>();
            inventory
                .HasItems
                .Returns(true);

            var findingResult = new CraftingFormationFindingResult(true, new CraftingFormation[1]);
            
            var formationFinder = Substitute.For<ICraftingFormationFinder>();
            formationFinder
                .Find(inventory)
                .Returns(findingResult);

            var formationValidator = Substitute.For<ICraftingFormationValidator>();
            formationValidator
                .Validate(default)
                .ReturnsForAnyArgs(true);
            
            var craftingManager = new CraftingManager(inventory, formationFinder, formationValidator);
            
            var contracts = craftingManager.TryGetContracts();

            Assert.IsNotEmpty(contracts);
        }

        [Test]
        public void Given_NoFormationsFound_When_TryGetContracts_Then_ReturnsEmptyContracts()
        {
            var inventory = Substitute.For<ICraftingInventory>();
            inventory
                .HasItems
                .Returns(true);
            
            var findingResult = new CraftingFormationFindingResult(false, new CraftingFormation[0]);
            
            var formationFinder = Substitute.For<ICraftingFormationFinder>();
            formationFinder
                .Find(inventory)
                .Returns(findingResult);
            
            var formationValidator = Substitute.For<ICraftingFormationValidator>();
            formationValidator
                .Validate(default)
                .ReturnsForAnyArgs(true);

            var craftingManager = new CraftingManager(inventory, formationFinder, formationValidator);
            
            var contracts = craftingManager.TryGetContracts();
            
            Assert.IsEmpty(contracts);
        }

        [Test]
        public void Given_FormationsNotValid_When_TryGetContracts_Then_ReturnsEmptyContracts()
        {
            var inventory = Substitute.For<ICraftingInventory>();
            inventory
                .HasItems
                .Returns(true);

            var findingResult = new CraftingFormationFindingResult(true, new CraftingFormation[1]);
            
            var formationFinder = Substitute.For<ICraftingFormationFinder>();
            formationFinder
                .Find(inventory)
                .Returns(findingResult);

            var formationValidator = Substitute.For<ICraftingFormationValidator>();
            formationValidator    
                .Validate(default)
                .ReturnsForAnyArgs(false);
            
            var craftingManager = new CraftingManager(inventory, formationFinder, formationValidator);
            
            var contracts = craftingManager.TryGetContracts();
            
            Assert.IsEmpty(contracts);
        }
    }
}