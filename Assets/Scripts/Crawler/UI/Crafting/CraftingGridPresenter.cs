using System;
using Crawler.Crafting;
using DefaultNamespace;

namespace Crawler.UI.Crafting
{
    public class CraftingGridPresenter : ICraftingGridPresenter
    {
        public event Action OnModelChanged;
        
        private readonly ICraftingGridView _view;
        private readonly ICraftingResultView _resultView;
        private readonly ICraftingInventoryItemViewModelConverter _viewModelConverter;
        
        public CraftingGridPresenter(ICraftingGridView view, ICraftingResultView resultView, 
            ICraftingInventoryItemViewModelConverter viewModelConverter)
        {
            _view = view;
            _resultView = resultView;
            _viewModelConverter = viewModelConverter;
        }
        public void Present()
        {
            var inventory = IocContainer.GetSingleton<ICraftingInventory>();
            var viewModels = _viewModelConverter.Convert(inventory.Nodes);

            _view.OnViewModelMoved -= OnViewModelMoved;
            _view.OnViewModelMoved += OnViewModelMoved;
            
            _view.Initialize(5, 6);
            _view.ClearItems();
            _view.SpawnItems(viewModels);

            _resultView.ShowResult(inventory);
        }

        // TODO: This needs to be tested.
        private void OnViewModelMoved((int x, int y) start, (int x, int y) target)
        {
            var inventory = IocContainer.GetSingleton<ICraftingInventory>();
            
            var moverIndex = FindIndexByCoordinates(start.x, start.y);

            if (moverIndex == -1)
            {
                return;
            }
            
            var moverIngredientType = inventory.Nodes[moverIndex].IngredientType;
            
            var occupierIndex = FindIndexByCoordinates(target.x, target.y);
            
            inventory.Nodes[moverIndex] = new CraftingInventoryItem(moverIngredientType, target.x, target.y);
            
            if (occupierIndex == -1)
            {
                IocContainer.RegisterSingleton(inventory);
                OnModelChanged?.Invoke();
                return;
            }

            var occupierIngredientType = inventory.Nodes[occupierIndex].IngredientType;
            
            inventory.Nodes[occupierIndex] = new CraftingInventoryItem(occupierIngredientType, start.x, start.y);
            
            IocContainer.RegisterSingleton(inventory);
            OnModelChanged?.Invoke();
        }

        private int FindIndexByCoordinates(int x, int y)
        {
            var inventory = IocContainer.GetSingleton<ICraftingInventory>();
            for (int i = 0; i < inventory.Nodes.Length; i++)
            {
                var node = inventory.Nodes[i];
                if (node.X == x && node.Y == y)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}