using Crawler.Crafting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Bootstrapper : MonoBehaviour
    {
        public void Awake()
        {
            var craftingInventory = GetCraftingInventory();
            IocContainer.RegisterSingleton(craftingInventory);
        }

        private ICraftingInventory GetCraftingInventory()
        {
            var craftingInventoryItems = new[]
            {
                new CraftingInventoryItem(0, 0, 0),
                new CraftingInventoryItem(1, 1, 1),
                new CraftingInventoryItem(2, 2, 2)
            };
            
            return new CraftingInventory(craftingInventoryItems);
        }
        
        public void Start()
        {
            SceneManager.LoadScene("CraftingView");
        }
    }
}