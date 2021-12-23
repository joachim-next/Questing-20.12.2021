using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Crawler.UI.Crafting
{
    public class CraftingInventoryItemViewMover : MonoBehaviour
    {
        public event Action<GameObject, GameObject> OnSwapped;
        
        [SerializeField] 
        private GraphicRaycaster _raycaster;
        [SerializeField] 
        private EventSystem _eventSystem;

        private CraftingInventoryItemView _selectedView;
        private Vector3 _selectedViewOrigin;
        private Transform _selectedViewParent;
    
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectView();
                SaveSelectedViewPosition();
                SaveSelectedViewParent();
                ChangeSelectedViewParent();
            }
            else if (Input.GetMouseButton(0))
            {
                MoveSelectedView();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                FireEvent();
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

        private void SaveSelectedViewPosition()
        {
            if (_selectedView == null)
            {
                return;
            }

            _selectedViewOrigin = _selectedView.transform.position;
        }

        private void SaveSelectedViewParent()
        {
            if (_selectedView == null)
            {
                return;
            }

            _selectedViewParent = _selectedView.transform.parent;
        }

        private void ChangeSelectedViewParent()
        {
            if (_selectedView == null)
            {
                return;
            }

            _selectedView.transform.SetParent(transform);
        }

        private void MoveSelectedView()
        {
            if (_selectedView == null)
            {
                return;
            }

            var mousePositionAtGridLevel = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 
                _selectedViewOrigin.z);
            _selectedView.transform.position = mousePositionAtGridLevel;
        }

        private void FireEvent()
        {
            if (_selectedView == null)
            {
                return;
            }

            var slot = GetSlotUnderMouseOrNull();

            if (slot == null)
            {
                return;
            }
            
            OnSwapped?.Invoke(_selectedViewParent.gameObject, slot);
        }

        private GameObject GetSlotUnderMouseOrNull()
        {
            GameObject go = null;

            var pointerEventData = new PointerEventData(_eventSystem);
            pointerEventData.position = Input.mousePosition;

            var raycastResults = new List<RaycastResult>();

            _raycaster.Raycast(pointerEventData, raycastResults);

            foreach (var result in raycastResults)
            {
                if (!result.gameObject.CompareTag("Slot"))
                {
                    continue;
                }

                go = result.gameObject;
            }
            
            return go;
        }

        private void Reset()
        {
            if (_selectedView == null)
            {
                return;
            }
            
            _selectedView.transform.position = _selectedViewOrigin;
            _selectedView.transform.SetParent(_selectedViewParent);
            _selectedView = null;
        }
    }
}
