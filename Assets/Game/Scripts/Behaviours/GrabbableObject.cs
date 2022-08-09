// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    /// <summary>
    /// 
    /// Set grabbable objects, rigidbodys interpolate value to -> Interpolate
    /// Set some drag value to grabbable objects Rigidbody, like '5' to avoid trembling effect
    /// which occurs when grabbable object pushed by another collider.
    /// 
    /// Ref : https://www.youtube.com/watch?v=2IhzPTS4av4&ab_channel=CodeMonkey
    /// </summary>
    [RequireComponent(typeof(Collider),typeof(Rigidbody))]
    public class GrabbableObject : MonoBehaviour
    {
        [Header("Grabbable Object Settings")]
        [SerializeField] private bool makeKinematicWhenGrabbed = false;

        [Header("Grabbable Properties")] 
        public int objectHeight;
        public List<GrabbableSlotBehaviour> grabbableSlotBehaviours;

        private Rigidbody _objectRigidbody;
        private Transform _objectGrabPointTransform;
        private float _lerpSpeed = 10f;

        private void Awake()
        {
            _objectRigidbody = GetComponent<Rigidbody>();
            
            if(objectHeight <= 0 )
            Debug.LogError("Object height must be greater than 0.");
        }

        public void Grab(Transform grabPointTransform)
        {
            if (makeKinematicWhenGrabbed) _objectRigidbody.isKinematic = true;
            _objectGrabPointTransform = grabPointTransform;
            _objectRigidbody.useGravity = false;
        }
        
        private void Update()
        {
            // DrawRayFromSlots();
        }
        
        private void FixedUpdate()
        {
            if(_objectGrabPointTransform == null) return;
            MoveObject();
        }
        
        public void SuccessDrop()
        {
            
        }

        public void FailDrop()
        {
            
        }
        
        // public void Drop()
        // {
        //     if (makeKinematicWhenGrabbed) _objectRigidbody.isKinematic = false;
        //     _objectGrabPointTransform = null;
        //     _objectRigidbody.useGravity = true;
        // }
        
        private void MoveObject()
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, _objectGrabPointTransform.position,_lerpSpeed * Time.deltaTime);
            _objectRigidbody.MovePosition(newPosition);
        }

        private void DrawRayFromSlots()
        {
            foreach (GrabbableSlotBehaviour grabbableSlot in grabbableSlotBehaviours)
            {
                Vector3 direction = grabbableSlot.transform.TransformDirection(Vector3.down) * 2;
                Debug.DrawRay(grabbableSlot.transform.position,direction,Color.green);
            }
        }
    }
}
