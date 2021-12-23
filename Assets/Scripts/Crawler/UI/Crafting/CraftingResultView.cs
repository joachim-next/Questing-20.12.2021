using System.Linq;
using Crawler.Crafting;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Crawler.UI.Crafting
{
    public class CraftingResultView : MonoBehaviour, ICraftingResultView
    {
        [SerializeField]
        private CraftingInventoryItemImageHolder _imageHolder;
        [SerializeField] 
        private TextMeshProUGUI _resultStateText;
        [SerializeField]
        private Image _image;
        
        public void ShowResult(ICraftingInventory inventory)
        {
            _resultStateText.text = string.Empty;
            _image.sprite = null;
            
            var formationFinder = IocContainer.GetSingleton<CraftingManager>();
            var contracts = formationFinder.TryGetContracts();

            _resultStateText.text = contracts.Length != 0 ? "Item ready to be crafted" : "Nothing to craft";
            _resultStateText.color = contracts.Length != 0 ? Color.green : Color.red;

            if (contracts.Length == 0)
            {
                return;
            }
            
            var contract = contracts[0];

            _image.sprite = _imageHolder.Entries
                .First(x 
                    => x.IngredientType == contract.ItemToBeCrafted.IngredientType)
                .Sprite;
        }
    }
}