using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Crawler.UI.Crafting
{
    public class CraftingInventoryItemViewMover : MonoBehaviour
    {
        [SerializeField] 
        private GraphicRaycaster _raycaster;
        [SerializeField] 
        private EventSystem _eventSystem;

        private CraftingInventoryItemView _selectedView;
    
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                SelectView();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Reset();
            }
        }

        private void SelectView()
        {
            var view = GetClickedViewOrNull();

            if (view == null)
            {
                return;
            }

            _selectedView = view;
        }

        private CraftingInventoryItemView GetClickedViewOrNull()
        {
            CraftingInventoryItemView view = null;

            var pointerEventData = new PointerEventData(_eventSystem);
            pointerEventData.position = Input.mousePosition;

            var raycastResults = new List<RaycastResult>();

            _raycaster.Raycast(pointerEventData, raycastResults);

            foreach (var result in raycastResults)
            {
                var component = result.gameObject.GetComponent<CraftingInventoryItemView>();

                if (component == null)
                {
                    continue;
                }
                
                view = component;
                break;
            }
            
            return view;
        }

        private void Reset()
        {
            _selectedView = null;
        }
    }
}
