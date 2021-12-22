using System;

namespace Crawler.UI.Crafting
{
    public interface ICraftingGridView
    {
        event Action<(int x, int y), (int x, int y)> OnViewModelMoved;
        void Initialize(int width, int height);
        void ClearItems();
        void SpawnItems(CraftingInventoryItemViewModel[] items);
    }
}