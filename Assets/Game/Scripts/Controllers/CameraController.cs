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

        private void Start()
        {
            carFollowerCamera.enabled = true;
            EnableGameCameraSetter(false);

            DOVirtual.DelayedCall(1.5f, () =>
            {
                EnableGameCameraSetter(true);
            });
        }

        public void EnableGameCameraSetter(bool status)
        {
            gameCamera.enabled = status;
        }
        
    }
}
