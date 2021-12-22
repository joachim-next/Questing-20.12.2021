using UnityEngine;

namespace Crawler.UI.Crafting
{
    public class CraftingGridView : MonoBehaviour, ICraftingGridView
    {
        [SerializeField] 
        private CraftingInventoryItemView _itemViewPrefab;

        public void Initialize(int width, int height)
        {
            
        }

        public void SpawnItems(CraftingInventoryItemViewModel[] items)
        {
            foreach (var item in items)
            {
                CreateInstance(_itemViewPrefab, item);
            }
        }

        private void CreateInstance(CraftingInventoryItemView view, CraftingInventoryItemViewModel viewModel)
        {
            var instance = Instantiate(view, transform);
            instance.Inject(viewModel);
        }
    }
}