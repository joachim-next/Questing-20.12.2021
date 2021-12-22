using UnityEngine;

namespace Crawler.UI.Crafting
{
    public class CraftingInventoryItemView : MonoBehaviour
    {
        public void Inject(CraftingInventoryItemViewModel viewModel)
        {
            Debug.Log("ViewModel injected!");
        }
    }
}