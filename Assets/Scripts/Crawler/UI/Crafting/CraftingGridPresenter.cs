using Crawler.Crafting;

namespace Crawler.UI.Crafting
{
    public class CraftingGridPresenter
    {
        private readonly ICraftingGridView _view;
        private readonly ICraftingInventory _inventory;
        
        public CraftingGridPresenter(ICraftingGridView view, ICraftingInventory inventory)
        {
            _view = view;
            _inventory = inventory;
        }
        public void Present()
        {
            var viewModels = ConvertToViewModels(_inventory.Nodes);
            
            _view.SpawnItems(viewModels);
        }

        private CraftingInventoryItemViewModel[] ConvertToViewModels(CraftingInventoryItem[] models)
        {
            return new CraftingInventoryItemViewModel[0];
        }
    }
}