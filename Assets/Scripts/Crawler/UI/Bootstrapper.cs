using Crawler.UI.Crafting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bootstrapper : MonoBehaviour
    {
        public void Start()
        {
            var craftingGridPresenter = IocContainer.GetSingleton<ICraftingGridPresenter>();
            
            craftingGridPresenter.Present();
        }
    }
}