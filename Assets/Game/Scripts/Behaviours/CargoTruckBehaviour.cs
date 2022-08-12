using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CargoTruckBehaviour : MonoBehaviour
{
    public Vector3 destinationPosition;
    [SerializeField] private float destinationDuration = 2f;
    public Transform luggageCarryPoint;
    public Transform luggageCarryPoint2;

    [HideInInspector] public Vector3 initialPosition;

    private bool _truckIsRecalled = false;
    [SerializeField] private GameObject additionalLuggagePack;

    private bool _firstRun=true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_truckIsRecalled) return;
            transform.position = new Vector3(100,0,-7);
            SetReDestination(destinationPosition);
            _truckIsRecalled = true;
        }
    }
    
    public void SetDestinationAndRun(Vector3 destination)
    {
        transform.DOMove(destination, destinationDuration).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            GameManager.Instance.SelectLuggageSet();
            GameManager.Instance.TruckLuggageSetter(false);

        });
        
    }

    public void SetReDestination(Vector3 destination)
    {
        GameManager.Instance.TruckLuggageSetter(true);
        transform.DOMove(new Vector3(0,0,-7), destinationDuration).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            additionalLuggagePack.SetActive(true);
            GameManager.Instance.TruckLuggageSetter(false);
            transform.DOMove(new Vector3(-100, 0, -7), destinationDuration).SetEase(Ease.InOutSine);
        });
    }
}
