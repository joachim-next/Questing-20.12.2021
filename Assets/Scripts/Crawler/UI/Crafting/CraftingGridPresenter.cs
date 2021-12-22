using Crawler.Crafting;

namespace Crawler.UI.Crafting
{
    public class CraftingGridPresenter : ICraftingGridPresenter
    {
        private readonly ICraftingGridView _view;
        private readonly ICraftingResultView _resultView;
        private readonly ICraftingInventory _inventory;
        private readonly ICraftingInventoryItemViewModelConverter _viewModelConverter;
        
        public CraftingGridPresenter(ICraftingGridView view, ICraftingResultView resultView, ICraftingInventory inventory, 
            ICraftingInventoryItemViewModelConverter viewModelConverter)
        {
            _view = view;
            _resultView = resultView;
            _inventory = inventory;
            _viewModelConverter = viewModelConverter;
        }
        public void Present()
        {
            var viewModels = _viewModelConverter.Convert(_inventory.Nodes);

            _view.Initialize(5, 6);
            _view.SpawnItems(viewModels);

            _resultView.ShowResult(_inventory);
        }
    }
}