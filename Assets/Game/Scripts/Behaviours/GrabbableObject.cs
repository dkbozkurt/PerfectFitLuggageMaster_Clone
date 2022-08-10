// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using System;
using DG.Tweening;
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
        [SerializeField] private bool makeKinematicWhenGrabbed = false;
        private Rigidbody _objectRigidbody;
        private Transform _objectGrabPointTransform;
        private float _lerpSpeed = 10f;
        private int angleToRotate = 0;

        private Vector3 _additionalOffSetValue = Vector3.zero;

        private void Awake()
        {
            _objectRigidbody = GetComponent<Rigidbody>();
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1))
            {
                RotateObject();
            }
        }
        
        private void FixedUpdate()
        {
            if(_objectGrabPointTransform == null) return;
            Vector3 newPosition = Vector3.Lerp(transform.position,
                _objectGrabPointTransform.position + _additionalOffSetValue,
                _lerpSpeed * Time.deltaTime);
            _objectRigidbody.MovePosition(newPosition);
        }
        
        public void Grab(Transform grabPointTransform)
        {
            if (makeKinematicWhenGrabbed) _objectRigidbody.isKinematic = true;
            _objectGrabPointTransform = grabPointTransform;
            _objectRigidbody.useGravity = false;
        }

        public void Drop()
        {
            if (makeKinematicWhenGrabbed) _objectRigidbody.isKinematic = false;
            _objectGrabPointTransform = null;
            _objectRigidbody.useGravity = true;
        }

        public void AddExtraOffSetValue(float value)
        {
            _additionalOffSetValue += new Vector3(0, value, 0);
        }
        
        private void RotateObject()
        {
            angleToRotate += 90;
            transform.DORotate(new Vector3(0,angleToRotate, 0), 0.2f).SetEase(Ease.Linear);
            
            // Vector3 newRotation = Vector3.Lerp(transform.rotation.eulerAngles, 
            //     transform.rotation.eulerAngles + new Vector3(0,10,0)
            //     ,0.1f * Time.deltaTime);
            // transform.Rotate(newRotation);
        }
    }
}
