// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using System;
using System.Collections.Generic;
using DG.Tweening;
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
        
        [Header("Highlight Properties")] 
        [SerializeField] private Texture highlightTexture; 
        
        private Rigidbody _objectRigidbody;
        [HideInInspector] public Transform objectGrabPointTransform;
        private float _lerpSpeed = 10f;

        [HideInInspector] public List<ItemSlotBehaviour> itemSlots;
        private Vector3 _pivotPointFirstPosition;

        private int _angleToRotate = 0;
        private bool _isDragging = false;
        private Vector3 _initialMousePosition;

        private MaterialFadeBehaviour _materialFadeBehaviour;
        
        private void Awake()
        {
            _objectRigidbody = GetComponent<Rigidbody>();
            _materialFadeBehaviour = GetComponent<MaterialFadeBehaviour>();

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

        private void OnMouseDown()
        {
            _initialMousePosition = Input.mousePosition;
        }
        
        private void FixedUpdate()
        {
            if(objectGrabPointTransform == null) return;

            if (!_isDragging && _initialMousePosition == Input.mousePosition) return;

            _isDragging = true;
            MoveObject();
        }

        private void OnMouseUp()
        {
            if (_isDragging)
            {
                _isDragging = false;
                return;
            }
            
            RotateObject();
        }

        public void SuccessDrop()
        {
            Debug.Log("Success Drop");
            
            if (grabbableSlotBehaviours.Count != itemSlots.Count)
            {
                FailDrop();
                return;
            }
            
            _materialFadeBehaviour.isPlaced = true;
            
            Vector3 pivotInterval = transform.localPosition - grabbableSlotBehaviours[0].transform.position;
            grabbableSlotBehaviours[0].transform.position = itemSlots[0].transform.position;
            transform.localPosition = grabbableSlotBehaviours[0].transform.position + pivotInterval;
            grabbableSlotBehaviours[0].transform.localPosition = _pivotPointFirstPosition;
            
        }

        public void FailDrop()
        {
            Debug.Log("Fail Drop");

            _materialFadeBehaviour.isPlaced = false;
            
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
            ItemHighlightBehaviour.Instance.HighlightSetter(true,gameObject);
            
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position,_lerpSpeed * Time.deltaTime);
            _objectRigidbody.MovePosition(newPosition);
        }

        private void RotateObject()
        {
            _angleToRotate += 90;
            transform.DORotate(new Vector3(0, _angleToRotate, 0), 0.2f).SetEase(Ease.Linear);
        }

        private void DrawRayFromSlots()
        {
            foreach (GrabbableSlotBehaviour grabbableSlot in grabbableSlotBehaviours)
            {
                Vector3 direction = grabbableSlot.transform.TransformDirection(Vector3.down * (GameManager.Instance.slotSizeMultiplier * GameManager.Instance.dragObjectOffsetValue.y));
                Debug.DrawRay(grabbableSlot.transform.position,direction,Color.green);
            }
        }
        
    }
}
