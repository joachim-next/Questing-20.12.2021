using Crawler.Crafting;
using Crawler.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Bootstrapper : MonoBehaviour
    {
        public CraftingContext CraftingContext;
        
        public void Awake()
        {
            var craftingInventory = GetCraftingInventory();
            IocContainer.RegisterSingleton(craftingInventory);

            var craftingManager = GetCraftingManager(craftingInventory);
            IocContainer.RegisterSingleton(craftingManager);
        }

        private ICraftingInventory GetCraftingInventory()
        {
            var craftingInventoryItems = new[]
            {
                new CraftingInventoryItem(1, 2, 1),
                new CraftingInventoryItem(1, 2, 2),
                new CraftingInventoryItem(2, 1, 3),
                new CraftingInventoryItem(2, 2, 3),
                new CraftingInventoryItem(2, 3, 3)
            };
            
            return new CraftingInventory(craftingInventoryItems);
        }

        private CraftingManager GetCraftingManager(ICraftingInventory inventory)
        {
            var formationFilter = new NotOwnedFormationFilter();
            var formationFinder = new CraftingFormationFinder(CraftingContext.FormationProvider, formationFilter);
            
            var bingoFormFactory = new CraftingInventoryNodeBingoFormFactory();
            var bingo = new CraftingInventoryNodeBingo();
            var contractResolver = new CraftingContractResolver(inventory, bingoFormFactory, bingo);
            
            return new CraftingManager(inventory, formationFinder, contractResolver);
        }
        
        public void Start()
        {
            SceneManager.LoadScene("CraftingView");
        }
    }
}