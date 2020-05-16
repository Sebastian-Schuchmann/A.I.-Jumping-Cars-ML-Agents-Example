using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Simple Enemy script. It just moves forward with a speed you determine
 */
public class Mover : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody Rigidbody;


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = Vector3.back * speed;
    }
}
