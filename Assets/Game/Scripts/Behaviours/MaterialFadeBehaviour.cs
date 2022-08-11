using System;
using DG.Tweening;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class MaterialFadeBehaviour : MonoBehaviour
    {
        private Material _objectMaterial;
        [HideInInspector] public bool isPlaced = false;
        
        private void Awake()
        {
            _objectMaterial = GetComponent<MeshRenderer>().material;
        }

        private void OnEnable()
        {
            PickUpDropManager.OnObjectDrop += InternalMaterialSetterToDefault;
        }

        private void OnDisable()
        {
            PickUpDropManager.OnObjectDrop -= InternalMaterialSetterToDefault;
        }

        private void OnMouseEnter()
        {
            if(!isPlaced || PickUpDropManager.Instance.grabbableObject == null) return;
            
            ChangeMaterialAlpha(0.2f);
        }

        private void OnMouseExit()
        {
            if(!isPlaced || PickUpDropManager.Instance.grabbableObject == null) return;
            
            ChangeMaterialAlpha(1);
        }

        private void InternalMaterialSetterToDefault()
        {
            ChangeMaterialAlpha(1);
        }
        
        private void ChangeMaterialAlpha(float value)
        {
            _objectMaterial.DOFade(value, 0.2f);
        }
    }
}
