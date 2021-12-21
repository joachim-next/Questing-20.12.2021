using System;
using Crawler.Crafting;

namespace Crawler.UI.Crafting
{
    public class CraftingInventoryViewModelConverter
    {
        public CraftingInventoryItemViewModel[] Convert(CraftingInventoryItem[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException($"Argument {nameof(models)} can't be null");
            }
            
            if (models.Length == 0)
            {
                throw new ArgumentException($"Argument {nameof(models)} can't be empty.");
            }
            return new CraftingInventoryItemViewModel[0];
        }
    }
}