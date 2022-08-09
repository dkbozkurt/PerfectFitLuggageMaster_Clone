// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using System;
using System.Collections.Generic;
using Game.Scripts.Managers;
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
        [HideInInspector] public Transform objectGrabPointTransform;
        private float _lerpSpeed = 10f;

        public List<ItemSlotBehaviour> itemSlots;
        private Vector3 _pivotPointFirstPosition;

        private void Awake()
        {
            _objectRigidbody = GetComponent<Rigidbody>();
            _pivotPointFirstPosition = grabbableSlotBehaviours[0].transform.localPosition;
            
            if(objectHeight <= 0 ) Debug.LogError("Object height must be greater than 0.");
        }

        public void Grab(Transform grabPointTransform)
        {
            if (makeKinematicWhenGrabbed) _objectRigidbody.isKinematic = true;
            objectGrabPointTransform = grabPointTransform;
            _objectRigidbody.useGravity = false;
        }
        
        private void Update()
        {
            DrawRayFromSlots();
        }
        
        private void FixedUpdate()
        {
            if(objectGrabPointTransform == null) return;
            MoveObject();
        }
        
        public void SuccessDrop()
        {
            Debug.Log("Success Drop");

            if (grabbableSlotBehaviours.Count != itemSlots.Count)
            {
                FailDrop();
                return;
            }
            
            Vector3 pivotInterval = transform.localPosition - grabbableSlotBehaviours[0].transform.position;
            grabbableSlotBehaviours[0].transform.position = itemSlots[0].transform.position;
            transform.localPosition = grabbableSlotBehaviours[0].transform.position + pivotInterval;
            grabbableSlotBehaviours[0].transform.localPosition = _pivotPointFirstPosition;
            
        }

        public void FailDrop()
        {
            Debug.Log("Fail Drop");
            transform.position = new Vector3(0, 1, 0);

            if (makeKinematicWhenGrabbed) _objectRigidbody.isKinematic = false;
            _objectRigidbody.velocity = Vector3.zero;
            objectGrabPointTransform = null;
            _objectRigidbody.useGravity = true;

        }
        
        // public void Drop()
        // {
        //     if (makeKinematicWhenGrabbed) _objectRigidbody.isKinematic = false;
        //     _objectGrabPointTransform = null;
        //     _objectRigidbody.useGravity = true;
        // }
        
        private void MoveObject()
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position,_lerpSpeed * Time.deltaTime);
            _objectRigidbody.MovePosition(newPosition);
        }

        private void DrawRayFromSlots()
        {
            foreach (GrabbableSlotBehaviour grabbableSlot in grabbableSlotBehaviours)
            {
                Vector3 direction = grabbableSlot.transform.TransformDirection(Vector3.down * GameManager.Instance.slotSizeMultiplier);
                Debug.DrawRay(grabbableSlot.transform.position,direction,Color.green);
            }
        }
    }
}
