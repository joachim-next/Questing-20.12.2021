using Crawler.Crafting;
using DefaultNamespace;
using UnityEngine;

namespace Crawler.UI.Crafting
{
    public class CraftingViewManager : MonoBehaviour
    {
        [SerializeField]
        private CraftingGridView _gridView;
        [SerializeField]
        private CraftingResultView _resultView;
        
        private ICraftingGridPresenter _gridPresenter;
        private ICraftingResultPresenter _resultPresenter;
     
        // USED ONLY IN TESTS
        public CraftingViewManager(ICraftingGridPresenter gridPresenter, ICraftingResultPresenter resultPresenter)
        {
            _gridPresenter = gridPresenter;
            _resultPresenter = resultPresenter;
        }

        public void Awake()
        {
            _gridPresenter = CreateGridPresenter();
        }

        private ICraftingGridPresenter CreateGridPresenter()
        {
            var craftingInventory = IocContainer.GetSingleton<ICraftingInventory>();
            var viewModelConverter = new CraftingInventoryViewModelConverter();
            
            return new CraftingGridPresenter(_gridView, _resultView, craftingInventory, viewModelConverter);
        }
        
        public void Start()
        {
            _gridPresenter.Present();
            _resultPresenter.Present();
        }
    }
}