using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Behaviours;
using UnityEngine;

public enum LuggageSet
{
    LuggageSet1,
    LuggageSet2,
    LuggageSet3
    
}
public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] private LuggageSet luggageSet;
    [SerializeField] private List<GameObject> luggageSets;

    private void Start()
    {
        SelectLuggageSet();
    }

    private void SelectLuggageSet()
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
    }
}
