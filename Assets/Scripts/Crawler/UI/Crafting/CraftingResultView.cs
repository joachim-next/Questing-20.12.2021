using Crawler.Crafting;
using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace Crawler.UI.Crafting
{
    public class CraftingResultView : MonoBehaviour, ICraftingResultView
    {
        [SerializeField] 
        private TextMeshProUGUI _resultStateText;
        
        public void ShowResult(ICraftingInventory inventory)
        {
            var formationFinder = IocContainer.GetSingleton<ICraftingFormationFinder>();

            var result = formationFinder.Find(inventory);

            _resultStateText.text = result.Success ? "Item ready to be crafted" : "Nothing to craft";
            _resultStateText.color = result.Success ? Color.green : Color.red;
        }
    }
}