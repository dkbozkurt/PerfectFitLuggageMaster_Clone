using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class ItemHighlightBehaviour : SingletonBehaviour<ItemHighlightBehaviour>
    {
        [SerializeField] private LayerMask interactableLayers;
        [SerializeField] private GameObject highlightSprite;
        
        private GameObject _highlightedObject;

        private void Awake()
        {
            highlightSprite.SetActive(false);    
        }

        public void HighlightSetter(bool status, GameObject grabbedObject=null)
        {
            if (status)
            {
                _highlightedObject = grabbedObject;
                SetHighlightProperties();    
            }
            highlightSprite.SetActive(status);
        }
        
        private void SetHighlightProperties()
        {
            transform.localScale = new Vector3(_highlightedObject.transform.localScale.x, transform.localScale.y, _highlightedObject.transform.localScale.z);
        }
        
        private void Update()
        {
            if (highlightSprite.activeSelf)
                UpdateHighlightLocation();

        }
        
        private void UpdateHighlightLocation()
        {
            if (Physics.Raycast(_highlightedObject.transform.position
                    ,Vector3.down //_highlightedObject.transform.TransformDirection(Vector3.down)
                    , out RaycastHit raycastHit,float.MaxValue
                    , interactableLayers))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * raycastHit.distance, Color.yellow);
                transform.position = new Vector3(_highlightedObject.transform.position.x,
                    raycastHit.point.y, _highlightedObject.transform.position.z);
                transform.rotation = Quaternion.Euler(
                    transform.rotation.x,
                    _highlightedObject.transform.rotation.eulerAngles.y,
                    transform.rotation.z);
                
            }
        }
    }
}
