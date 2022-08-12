using System;
using Cinemachine;
using DG.Tweening;
using Game.Scripts.Behaviours;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [Header("Cameras")] 
        [SerializeField] private CinemachineVirtualCamera gameCamera;
        [SerializeField] private CinemachineVirtualCamera carFollowerCamera;
        [SerializeField] private CinemachineVirtualCamera creativeCamera;

        private bool _creativeCamStatus = false;
        private void Start()
        {
            carFollowerCamera.enabled = true;
            EnableGameCameraSetter(false);

            DOVirtual.DelayedCall(1.5f, () =>
            {
                EnableGameCameraSetter(true);
            });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _creativeCamStatus = !_creativeCamStatus;
                AdditionalCamSetter(_creativeCamStatus);
            }
        }

        private void AdditionalCamSetter(bool status)
        {
            creativeCamera.enabled = status;
        }

        public void EnableGameCameraSetter(bool status)
        {
            gameCamera.enabled = status;
        }
        
    }
}
