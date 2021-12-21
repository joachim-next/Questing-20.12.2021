using Crawler.Crafting;

namespace Crawler.UI.Crafting
{
    public class CraftingGridPresenter : ICraftingGridPresenter
    {
        private readonly ICraftingGridView _view;
        private readonly ICraftingInventory _inventory;
        private readonly ICraftingInventoryItemViewModelConverter _viewModelConverter;
        
        public CraftingGridPresenter(ICraftingGridView view, ICraftingInventory inventory, 
            ICraftingInventoryItemViewModelConverter viewModelConverter)
        {
            _view = view;
            _inventory = inventory;
            _viewModelConverter = viewModelConverter;
        }
        public void Present()
        {
            var viewModels = _viewModelConverter.Convert(_inventory.Nodes);
            
            _view.SpawnItems(viewModels);
        }
    }
}