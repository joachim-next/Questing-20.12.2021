using System;
using Crawler.Crafting;

namespace Crawler.UI.Crafting
{
    public class CraftingGridPresenter : ICraftingGridPresenter
    {
        private readonly ICraftingGridView _view;
        private readonly ICraftingResultView _resultView;
        private readonly ICraftingInventory _inventory;
        private readonly ICraftingInventoryItemViewModelConverter _viewModelConverter;
        private readonly Action<ICraftingInventory> _onModelChanged;
        
        public CraftingGridPresenter(ICraftingGridView view, ICraftingResultView resultView, ICraftingInventory inventory, 
            ICraftingInventoryItemViewModelConverter viewModelConverter, Action<ICraftingInventory> onModelChanged)
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
            _onModelChanged(default);
        }
    }
}