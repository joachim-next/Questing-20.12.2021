using Crawler.Crafting;

namespace Crawler.UI.Crafting.Tests
{
    public interface ICraftingInventoryItemViewModelConverter
    {
        CraftingInventoryItemViewModel[] Convert(CraftingInventoryItem[] models);
    }
}