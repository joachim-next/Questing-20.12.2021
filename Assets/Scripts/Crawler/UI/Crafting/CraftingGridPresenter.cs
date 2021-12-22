using System;
using Crawler.Crafting;
using DefaultNamespace;

namespace Crawler.UI.Crafting
{
    public class CraftingGridPresenter : ICraftingGridPresenter
    {
        private readonly ICraftingGridView _view;
        private readonly ICraftingResultView _resultView;
        private readonly ICraftingInventory _inventory;
        private readonly ICraftingInventoryItemViewModelConverter _viewModelConverter;
        private readonly Action _onModelChanged;
        
        public CraftingGridPresenter(ICraftingGridView view, ICraftingResultView resultView, ICraftingInventory inventory, 
            ICraftingInventoryItemViewModelConverter viewModelConverter, Action onModelChanged)
        {
            onModelChanged.ThrowIfNull(nameof(onModelChanged));

            _view = view;
            _resultView = resultView;
            _inventory = inventory;
            _viewModelConverter = viewModelConverter;
            _onModelChanged = onModelChanged;
        }
        public void Present()
        {
            var viewModels = _viewModelConverter.Convert(_inventory.Nodes);

            _view.Initialize(5, 6);
            _view.SpawnItems(viewModels);

            _resultView.ShowResult(_inventory);
        }

        public void UpdateModel(CraftingInventoryItemViewModel[] viewModels)
        {
            var updatedInventory = InventoryFrom(viewModels);
            IocContainer.RegisterSingleton(updatedInventory);
            
            _onModelChanged();
        }

        private ICraftingInventory InventoryFrom(CraftingInventoryItemViewModel[] viewModels)
        {
            var models = new CraftingInventoryItem[viewModels.Length];
            for(int i = 0; i < viewModels.Length; i++)
            {
                var viewModel = viewModels[i];
                var model = new CraftingInventoryItem(viewModel.IngredientType, viewModel.X, viewModel.Y);

                models[i] = model;
            }
            
            return new CraftingInventory(models);
        }
    }
}