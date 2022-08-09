﻿// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Managers;
using UnityEngine;

public class MousePositionBehaviour : MonoBehaviour
{

    [SerializeField] private LayerMask mouseColliderForLayerMask;
    private Vector3 _offSetValue;
    private Camera mainCamera;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        _offSetValue = GameManager.Instance.dragObjectOffsetValue;
    }
    
    void Update ()
    {
        if(Input.GetMouseButton(0)) MouseFollower3DwithLayer();
    }

    private void MouseFollower3DwithLayer()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue,mouseColliderForLayerMask) )
        {
            transform.position = raycastHit.point + _offSetValue;
        }
    }
}
