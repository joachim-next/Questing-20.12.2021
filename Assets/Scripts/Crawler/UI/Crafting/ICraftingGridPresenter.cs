using System;

namespace Crawler.UI.Crafting
{
    public interface ICraftingGridPresenter : IViewPresenter
    {
        event Action OnModelChanged;
    }
}