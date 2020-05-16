using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomColorOnSpawn : MonoBehaviour
{
    [SerializeField] private int materialIndexToRandomize;
    
    private void Awake()
    {
        GetComponent<MeshRenderer>().materials[materialIndexToRandomize].color = Random.ColorHSV();
    }
}
