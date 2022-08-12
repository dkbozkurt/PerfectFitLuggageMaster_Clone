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

    private void OnEnable()
    {
        // SpawnLuggageSetOnTruck();
    }

    public void SelectLuggageSet()
    {
        luggageSets[(int)luggageSet].SetActive(true);
        
        DOVirtual.DelayedCall(0.5f, () =>
        {
            cargoTruck.GetComponent<CargoTruckBehaviour>().SetDestinationAndRun(new Vector3(-100, 0, -6));
        });
        
    }

    private void SpawnLuggageSetOnTruck()
    {
        var selectedLuggage = luggageSets[(int) luggageSet];
        selectedLuggage.SetActive(true);
        var luggages = Instantiate(luggageSets[(int) luggageSet],cargoTruck.GetComponent<CargoTruckBehaviour>().luggageCarryPoint);
        
        var grabbableObjectsRB = luggages.GetComponentsInChildren<Rigidbody>();
        foreach (var child in grabbableObjectsRB)
        {
            child.isKinematic = true;
        }

    }
}