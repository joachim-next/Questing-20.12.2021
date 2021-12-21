using Crawler.Crafting;

namespace Crawler.UI.Crafting
{
    public interface ICraftingInventoryItemViewModelConverter
    {
        CraftingInventoryItemViewModel[] Convert(CraftingInventoryItem[] models);
    }
}