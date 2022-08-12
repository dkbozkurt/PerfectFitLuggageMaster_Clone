﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CargoTruckBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 destinationPosition;
    [SerializeField] private float destinationDuration = 2f;
    
    private void OnEnable()
    {
        DOVirtual.DelayedCall(0.5f, () =>
        {
            SetDestinationAndRun(destinationPosition);
        });
    }

    public void SetDestinationAndRun(Vector3 destination)
    {
        transform.DOMove(destination, destinationDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            GameManager.Instance.SelectLuggageSet();
            
        });
        
    }
}