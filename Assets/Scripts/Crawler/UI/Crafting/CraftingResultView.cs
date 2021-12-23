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
            var formationFinder = IocContainer.GetSingleton<CraftingManager>();

            var contracts = formationFinder.TryGetContracts();

            _resultStateText.text = contracts.Length != 0 ? "Item ready to be crafted" : "Nothing to craft";
            _resultStateText.color = contracts.Length != 0 ? Color.green : Color.red;
        }
    }
}