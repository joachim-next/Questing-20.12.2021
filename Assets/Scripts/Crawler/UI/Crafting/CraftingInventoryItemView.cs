using UnityEngine;
using TMPro;

namespace Crawler.UI.Crafting
{
    public class CraftingInventoryItemView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _ingredientTypeText;
            

        public void Inject(CraftingInventoryItemViewModel viewModel)
        {
            _ingredientTypeText.text = viewModel.IngredientType.ToString();
        }
    }
}