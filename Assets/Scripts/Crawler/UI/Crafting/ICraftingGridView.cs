namespace Crawler.UI.Crafting
{
    public interface ICraftingGridView
    {
        void Initialize(int width, int height);
        void SpawnItems(CraftingInventoryItemViewModel[] items);
    }
}