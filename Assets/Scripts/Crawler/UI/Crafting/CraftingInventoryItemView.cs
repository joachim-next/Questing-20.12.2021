using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Crawler.UI.Crafting
{
    public class CraftingInventoryItemView : MonoBehaviour
    {
        [SerializeField] 
        private Image _image;

        [SerializeField] 
        private CraftingInventoryItemImageHolder ImageHolder;

        public void Inject(CraftingInventoryItemViewModel viewModel)
        {
            _image.sprite = ImageHolder.Entries
                .First(x => x.IngredientType == viewModel.IngredientType).Sprite;
        }
    }
}