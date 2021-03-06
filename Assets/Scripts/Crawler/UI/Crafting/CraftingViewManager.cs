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
            _resultPresenter = CreateResultPresenter();
            
            RegisterOnModelChanged();
        }

        private void RegisterOnModelChanged()
        {
            _gridPresenter.OnModelChanged += UpdatePresentation;
        }

        private ICraftingGridPresenter CreateGridPresenter()
        {
            var viewModelConverter = new CraftingInventoryViewModelConverter();
            
            return new CraftingGridPresenter(_gridView, _resultView, viewModelConverter);
        }

        private ICraftingResultPresenter CreateResultPresenter()
        {
            var craftingInventory = IocContainer.GetSingleton<ICraftingInventory>();
            
            return new CraftingResultPresenter(_resultView, craftingInventory);
        }
        
        public void Start()
        {
            UpdatePresentation();
        }

        private void UpdatePresentation()
        {
            _gridPresenter.Present();
            _resultPresenter.Present();
        }
    }
}