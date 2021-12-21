using Crawler.Crafting;
using Crawler.UI.Crafting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bootstrapper : MonoBehaviour
    {
        public void Awake()
        {
            var craftingGridPresenter = CreateCraftingViewPresenter();
            IocContainer.RegisterSingleton(craftingGridPresenter);
        }

        private ICraftingGridPresenter CreateCraftingViewPresenter()
        {
            var craftingView = new GameObject("CraftingGridView")
                .AddComponent<CraftingGridView>();

            var inventoryItems = new[]
            {
                new CraftingInventoryItem(0, 0, 0),
                new CraftingInventoryItem(1, 1, 1),
                new CraftingInventoryItem(2, 2, 2)
            };
            var inventory = new CraftingInventory(inventoryItems);
            
            var craftingInventoryItemViewModelConverter = new CraftingInventoryViewModelConverter();

            return new CraftingGridPresenter(craftingView, inventory,
                craftingInventoryItemViewModelConverter);
        }

        public void Start()
        {
            var craftingGridPresenter = IocContainer.GetSingleton<ICraftingGridPresenter>();
            
            craftingGridPresenter.Present();
        }
    }
}