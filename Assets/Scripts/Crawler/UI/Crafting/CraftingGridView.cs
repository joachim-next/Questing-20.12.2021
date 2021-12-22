using UnityEngine;
using UnityEngine.UI;

namespace Crawler.UI.Crafting
{
    public class CraftingGridView : MonoBehaviour, ICraftingGridView
    {
        [SerializeField] 
        private GridLayoutGroup _grid;

        private GameObject[] _slotInstances;
        
        [Header("Prefabs")] 
        [SerializeField] private GameObject _gridSlotPrefab;
        [SerializeField] 
        private CraftingInventoryItemView _itemViewPrefab;

        public void Initialize(int width, int height)
        {
            _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _grid.constraintCount = width;
            
            SpawnSlots(width * height);
        }

        private void SpawnSlots(int count)
        {
            _slotInstances = new GameObject[count];
            
            for (int i = 0; i < count; i++)
            {
                _slotInstances[i] = Instantiate(_gridSlotPrefab, transform);
            }
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
            var parent = GetParentSlot(viewModel);
            var instance = Instantiate(view, parent);
            instance.Inject(viewModel);
        }

        private Transform GetParentSlot(CraftingInventoryItemViewModel viewModel)
        {
            var index = GetSlotIndex(viewModel.X, viewModel.Y);

            return _slotInstances[index].transform;
        }
        
        private int GetSlotIndex(int x, int y)
        {
            var lastChildIndex = _grid.transform.childCount - 1;
            var gridWidth = _grid.constraintCount;

            var slotNumber = x + y * gridWidth;
            return lastChildIndex - slotNumber;
        }
    }
}