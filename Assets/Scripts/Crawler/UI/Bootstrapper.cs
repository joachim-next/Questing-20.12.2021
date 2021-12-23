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

            var craftingFormationFinder = GetCraftingFormationFinder();
            IocContainer.RegisterSingleton(craftingFormationFinder);
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

        private ICraftingFormationFinder GetCraftingFormationFinder()
        {
            var formationFilter = new NotOwnedFormationFilter();

            return new CraftingFormationFinder(CraftingContext.FormationProvider, formationFilter);
        }
        
        public void Start()
        {
            SceneManager.LoadScene("CraftingView");
        }
    }
}