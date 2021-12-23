using System;
using UnityEngine;
using UnityEngine.UI;

namespace Crawler.UI.Crafting
{
    public class CraftingGridView : MonoBehaviour, ICraftingGridView
    {
        public event Action<(int x, int y), (int x, int y)> OnViewModelMoved;
        
        [SerializeField] 
        private GridLayoutGroup _grid;
        [SerializeField]
        private CraftingInventoryItemViewMover _viewMover;
        
        [Header("Prefabs")] 
        [SerializeField] 
        private GameObject _gridSlotPrefab;
        [SerializeField] 
        private CraftingInventoryItemView _itemViewPrefab;

        private const int NotFoundIndex = -1;   
        
        private GameObject[] _slotInstances;
        private GameObject[] _itemInstances;
        
        public void Awake()
        {
            _viewMover.OnSwapped += (fromSlot, toSlot) =>
            {
                var fromIndex = IndexOfObject(fromSlot, _slotInstances);
                var fromCoordinates = GetCoordinates(fromIndex);

                var toSlotIndex = IndexOfObject(toSlot, _slotInstances);
                var toCoordinates = GetCoordinates(toSlotIndex);
                
                OnViewModelMoved?.Invoke(fromCoordinates, toCoordinates);
            };
        }

        private int IndexOfObject(GameObject item, GameObject[] collection)
        {
            for (int i = 0; i < collection.Length; i++)
            {
                if (collection[i] == item)
                {
                    return i;
                }
            }
            
            throw new InvalidOperationException($"{nameof(item)} not found in {nameof(collection)}.");
        }
        
        private (int x, int y) GetCoordinates(int index)
        {
            var width = _grid.constraintCount;
            
            var tempX = index % width;
            var tempY = (int) Math.Floor((float)index / width);
            
            return (tempX, tempY);
        }

        public void Initialize(int width, int height)
        {
            _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _grid.constraintCount = width;

            ClearSlots();
            SpawnSlots(width * height);
        }

        private void ClearSlots()
        {
            if (_slotInstances == null)
            {
                return;
            }
            
            foreach (var slot in _slotInstances)
            {
                Destroy(slot);
            }

            _slotInstances = null;
        }
        private void SpawnSlots(int count)
        {
            _slotInstances = new GameObject[count];
            
            for (int i = 0; i < count; i++)
            {
                _slotInstances[i] = Instantiate(_gridSlotPrefab, transform);
            }
        }
        
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnViewModelMoved?.Invoke((2, 1), (0, 0));
            }
        }

        public void ClearItems()
        {
            if (_itemInstances == null)
            {
                return;
            }
            
            foreach (var item in _itemInstances)
            {
                Destroy(item.gameObject);
            }

            _itemInstances = null;
        }
        
        public void SpawnItems(CraftingInventoryItemViewModel[] items)
        {
            _itemInstances = new GameObject[items.Length];
            for(int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                CreateInstance(_itemViewPrefab, item, i);
            }
        }

        private void CreateInstance(CraftingInventoryItemView view, CraftingInventoryItemViewModel viewModel, int index)
        {
            var parent = GetParentSlot(viewModel);
            var instance = Instantiate(view, parent);
            instance.Inject(viewModel);

            _itemInstances[index] = instance.gameObject;
        }

        private Transform GetParentSlot(CraftingInventoryItemViewModel viewModel)
        {
            var index = GetSlotIndex(viewModel.X, viewModel.Y);

            return _slotInstances[index].transform;
        }
        
        private int GetSlotIndex(int x, int y)
        {
            var gridWidth = _grid.constraintCount;
            
            return x + y * gridWidth;
        }
    }
}