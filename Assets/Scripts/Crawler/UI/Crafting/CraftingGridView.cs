using UnityEngine;

namespace Crawler.UI.Crafting
{
    public class CraftingGridView : MonoBehaviour, ICraftingGridView
    {
        [SerializeField] 
        private CraftingInventoryItemView _itemViewPrefab;
        
        public void SpawnItems(CraftingInventoryItemViewModel[] items)
        {
            foreach (var item in items)
            {
                CreateInstance(_itemViewPrefab, item);
            }
        }

        private void CreateInstance(CraftingInventoryItemView view, CraftingInventoryItemViewModel viewModel)
        {
            var instance = Instantiate(view);
            instance.Inject(viewModel);
        }
    }
}