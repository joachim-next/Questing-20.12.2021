using Crawler.Crafting;

namespace Crawler.UI.Crafting
{
    public class CraftingResultPresenter : ICraftingResultPresenter
    {
        private readonly ICraftingResultView _view;
        private readonly ICraftingInventory _inventory;

        public CraftingResultPresenter(ICraftingResultView view, ICraftingInventory inventory)
        {
            _view = view;
            _inventory = inventory;
        }
        
        public void Present()
        {
            _view.ShowResult(_inventory);
        }
    }
}