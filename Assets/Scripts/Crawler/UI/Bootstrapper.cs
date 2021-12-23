using Crawler.Crafting;
using Crawler.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] 
        private CraftingContext _craftingContext;
        
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
            var nodeBingoFormFactory = new CraftingInventoryNodeBingoFormFactory();
            var nodeBingo = new CraftingInventoryNodeBingo();

            return new CraftingFormationFinder(_craftingContext.FormationProvider, formationFilter, 
                nodeBingoFormFactory, nodeBingo);
        }
        
        public void Start()
        {
            SceneManager.LoadScene("CraftingView");
        }
    }
}