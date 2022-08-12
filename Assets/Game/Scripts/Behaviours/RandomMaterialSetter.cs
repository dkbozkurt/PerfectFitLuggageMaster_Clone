using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMaterialSetter : MonoBehaviour
{
    [SerializeField] private List<Material> assignableMats;
    [SerializeField] private int materialIndexToChange;

    private void Awake()
    {
        SetRandomMaterial();   
    }

    private void SetRandomMaterial()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().materials[materialIndexToChange].color = assignableMats[GetRandomNumber()].color;
    }

    private int GetRandomNumber()
    {
        return Random.Range(0, assignableMats.Count);
    }
}
