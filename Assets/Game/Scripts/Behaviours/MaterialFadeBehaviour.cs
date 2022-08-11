using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class MaterialFadeBehaviour : MonoBehaviour
    {
        private Material _objectMaterial;
        public bool isPlaced = false;
        
        private void Awake()
        {
            _objectMaterial = GetComponent<MeshRenderer>().material;
        }

        private void OnMouseEnter()
        {
            if(!isPlaced) return;
            Debug.Log("Material to Transparant");
            _objectMaterial.DOFade(0.3f, 0.2f);
        }

        private void OnMouseExit()
        {
            if(!isPlaced) return;
            Debug.Log("Material to normal");
            _objectMaterial.DOFade(1, 0.2f);
        }
    }
}
