using UnityEngine;

namespace Crawler.UI
{
    [CreateAssetMenu(fileName = nameof(CraftingInventoryItemImageHolder), 
        menuName = nameof(CraftingInventoryItemImageHolder))]
    public class CraftingInventoryItemImageHolder : ScriptableObject
    {
        public CraftingInventoryItemImageEntry[] Entries;
    }
}
