// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using System;
using System.Collections.Generic;
using Game.Scripts.Behaviours;
using UnityEngine;

namespace Game.Scripts.Managers
{
    /// <summary>
    /// Ref : https://www.youtube.com/watch?v=2IhzPTS4av4&ab_channel=CodeMonkey
    /// </summary>
    public class PickUpDropManager : SingletonBehaviour<PickUpDropManager>
    {
        [Header("Player Related")] 
        [SerializeField] private Transform objectGrabPointTransform;
        
        [Space]
        [Header("Grabbable Related")]
        [SerializeField] private LayerMask pickUpLayerMask;
        [SerializeField] private LayerMask itemSlotCollideLayerMask;
        private GrabbableObject _grabbableObject;
        private float _pickUpDistance = 100f;
        
        private Camera _mainCamera;
        
        private int _matchedSlots=0;
        private List<ItemSlotBehaviour> itemSlots;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(_grabbableObject) return;
                PickUpObject();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if(!_grabbableObject) return;
                _grabbableObject.objectGrabPointTransform = null;
                DropObject();
            }

        }

        private void PickUpObject()
        {
            // With Camera transform forward direction
            // if (Physics.Raycast(_mainCamera.transform.position ,_mainCamera.forward , out RaycastHit raycastHit,float.MaxValue, pickUpLayerMask))
            
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit,float.MaxValue, pickUpLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out _grabbableObject))
                {
                    _grabbableObject.Grab(objectGrabPointTransform);
                }
            }
        }

        private void DropObject()
        {
            ItemSlotsSetter(_grabbableObject.itemSlots,false);
            _grabbableObject.itemSlots.Clear();
            
            ItemHighlightBehaviour.Instance.HighlightSetter(false);
            
            if (CheckSlotsMatching())
            {
                _grabbableObject.SuccessDrop();
                //ShiftItemSlots(_grabbableObject.itemSlots);
                ItemSlotsSetter(_grabbableObject.itemSlots,true);
            }
            else
            {
                _grabbableObject.FailDrop();
                ItemSlotsSetter(_grabbableObject.itemSlots,false);
            }
            
            _grabbableObject = null;
        }
        
        private bool CheckSlotsMatching()
        {
            _matchedSlots = 0;
            
            foreach (GrabbableSlotBehaviour grabbableSlot in _grabbableObject.grabbableSlotBehaviours)
            {
                if (Physics.Raycast(grabbableSlot.transform.position,
                        -1 *  grabbableSlot.transform.up,
                        out RaycastHit raycastHit, 
                        GameManager.Instance.slotSizeMultiplier * GameManager.Instance.dragObjectOffsetValue.y,
                        itemSlotCollideLayerMask))
                {
                    
                    if (raycastHit.collider.GetComponent<ItemSlotBehaviour>().IsOccupied) break;
                    _grabbableObject.itemSlots.Add(raycastHit.collider.GetComponent<ItemSlotBehaviour>());
                    _matchedSlots++;
                }
            }

            if (_matchedSlots >= _grabbableObject.grabbableSlotBehaviours.Count)
                return true;
            else
            {
                return false;    
            }
            
        }

        private void ItemSlotsSetter(List<ItemSlotBehaviour> itemSlots,bool status)
        {
            foreach (var slot in itemSlots)
            {
                slot.IsOccupied = status;
            }
            
        }
        
        // private void ShiftItemSlots(List<ItemSlotBehaviour> itemSlots)
        // {
        //     foreach (var slot in itemSlots)
        //     {
        //         slot.transform.position = new Vector3(slot.transform.position.x,slot.transform.position.y +
        //             _grabbableObject.objectHeight * GameManager.Instance.slotSizeMultiplier, slot.transform.position.z);
        //     }
        // }
    }
}
