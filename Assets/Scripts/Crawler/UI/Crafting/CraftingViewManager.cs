using UnityEngine;

namespace Crawler.UI.Crafting
{
    public class CraftingViewManager : MonoBehaviour
    {
        private ICraftingGridPresenter _gridPresenter;
     
        // USED ONLY IN TESTS
        public CraftingViewManager(ICraftingGridPresenter gridPresenter)
        {
            _gridPresenter = gridPresenter;
        }
        
        public void Start()
        {
            _gridPresenter.Present();
        }
    }
}