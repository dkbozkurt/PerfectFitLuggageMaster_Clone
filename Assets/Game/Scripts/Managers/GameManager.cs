using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Scripts.Behaviours;
using Game.Scripts.Controllers;
using UnityEditor.SceneManagement;
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

    private GameObject _firstSpawnedLuggagesOnTruck;

    private void OnEnable()
    {
        _firstSpawnedLuggagesOnTruck = SpawnLuggageSetOnTruck(cargoTruck.GetComponent<CargoTruckBehaviour>().luggageCarryPoint);
        SpawnLuggageSetOnTruck(cargoTruck.GetComponent<CargoTruckBehaviour>().luggageCarryPoint2);
    }

    public void DisableTruckLuggage()
    {
        _firstSpawnedLuggagesOnTruck.SetActive(false);
    }

    public void SelectLuggageSet()
    {
        luggageSets[(int)luggageSet].SetActive(true);
        
        DOVirtual.DelayedCall(0.5f, () =>
        {
            cargoTruck.GetComponent<CargoTruckBehaviour>().SetDestinationAndRun(new Vector3(-100, 0, -6));
        });
        
    }
    
    

    private GameObject SpawnLuggageSetOnTruck(Transform spawnPoint)
    {
        var selectedLuggage = luggageSets[(int) luggageSet];
        var luggages = Instantiate(selectedLuggage, spawnPoint);
        luggages.SetActive(true);
        
        foreach (Transform child in luggages.transform)
        {
            Destroy(child.GetComponent<GrabbableObject>());
            Destroy(child.GetComponent<Rigidbody>());
            Destroy(child.GetComponent<Collider>());
        }

        return luggages;
    }
}