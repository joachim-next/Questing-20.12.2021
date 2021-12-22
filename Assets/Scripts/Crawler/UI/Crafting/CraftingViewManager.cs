using Crawler.Crafting;
using DefaultNamespace;
using UnityEngine;

namespace Crawler.UI.Crafting
{
    public class CraftingViewManager : MonoBehaviour
    {
        [SerializeField]
        private CraftingGridView _gridView;
        
        private ICraftingGridPresenter _gridPresenter;
     
        // USED ONLY IN TESTS
        public CraftingViewManager(ICraftingGridPresenter gridPresenter)
        {
            _gridPresenter = gridPresenter;
        }

        public void Awake()
        {
            _gridPresenter = CreateGridPresenter();
        }

        private ICraftingGridPresenter CreateGridPresenter()
        {
            var craftingInventory = IocContainer.GetSingleton<ICraftingInventory>();
            var viewModelConverter = new CraftingInventoryViewModelConverter();
            
            return new CraftingGridPresenter(_gridView, craftingInventory, viewModelConverter);
        }
        
        public void Start()
        {
            _gridPresenter.Present();
        }
    }
}