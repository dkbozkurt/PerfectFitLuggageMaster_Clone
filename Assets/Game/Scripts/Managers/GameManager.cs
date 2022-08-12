using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Scripts.Behaviours;
using Game.Scripts.Controllers;
using UnityEngine;

public enum LuggageSet
{
    LuggageSet1,
    LuggageSet2,
    LuggageSet3
}

public class GameManager : SingletonBehaviour<GameManager>
{
    [Header("Luggage")] [SerializeField] private LuggageSet luggageSet;
    [SerializeField] private List<GameObject> luggageSets;

    [Header("Game Flow")] [SerializeField] private GameObject cargoTruck;

    [SerializeField] private CameraController cameraController;

    public void SelectLuggageSet()
    {
        switch (luggageSet)
        {
            case LuggageSet.LuggageSet1:
                luggageSets[0].SetActive(true);
                break;

            case LuggageSet.LuggageSet2:
                luggageSets[1].SetActive(true);
                break;

            case LuggageSet.LuggageSet3:
                luggageSets[2].SetActive(true);
                break;

            default:
                Debug.LogError("Select Luggage Set Type !!!");
                break;
        }
        
        cameraController.EnableGameCameraSetter(true);
        
        DOVirtual.DelayedCall(0.5f, () =>
        {
            cargoTruck.GetComponent<CargoTruckBehaviour>().SetDestinationAndRun(new Vector3(-100, 0, -6));
        });
        
    }
}