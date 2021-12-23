using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Crawler.UI.Crafting
{
    public class CraftingInventoryItemView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _ingredientTypeText;
        [SerializeField] 
        private Image _image;

        [SerializeField] 
        private CraftingInventoryItemImageHolder ImageHolder;

        public void Inject(CraftingInventoryItemViewModel viewModel)
        {
            _ingredientTypeText.text = viewModel.IngredientType.ToString();
            _image.sprite = ImageHolder.Entries
                .First(x => x.IngredientType == viewModel.IngredientType).Sprite;
        }
    }
}