// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using System;
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
        [SerializeField] private Transform playerCameraTransform;
        [SerializeField] private Transform objectGrabPointTransform;
        
        [Space]
        [Header("Grabbable Related")]
        [SerializeField] private LayerMask pickUpLayerMask;
        private GrabbableObject _grabbableObject;
        private float _pickUpDistance = 100f;
        
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
                DropObject();
            }

        }

        private void PickUpObject()
        {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward,
                    out RaycastHit raycastHit, _pickUpDistance, pickUpLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out _grabbableObject))
                {
                    _grabbableObject.Grab(objectGrabPointTransform);
                }
            }
        }

        private void DropObject()
        {
            _grabbableObject.Drop();
            _grabbableObject = null;
        }
    }
}
