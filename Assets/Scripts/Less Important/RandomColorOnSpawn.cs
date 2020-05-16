using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Simple class to randomize the appearance of the Movers
 */
public class RandomColorOnSpawn : MonoBehaviour
{
    [SerializeField] private int materialIndexToRandomize;
    
    private void Awake()
    {
        GetComponent<MeshRenderer>().materials[materialIndexToRandomize].color = Random.ColorHSV();
    }
}
