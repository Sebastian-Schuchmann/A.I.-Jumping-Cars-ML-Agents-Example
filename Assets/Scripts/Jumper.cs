using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private KeyCode jumpKey;
    
    private bool jumpIsReady = true;
    private Rigidbody rBody;
    private Vector3 startingPosition;
    private int score = 0;
    public event Action OnReset;
    
    public void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        startingPosition = transform.position;
    }
    
    private void Jump()
    {
        if (jumpIsReady)
        {
            rBody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
            jumpIsReady = false;
        }
    }

    private void Update()
    {
        if (Input.GetKey(jumpKey))
            Jump();
    }

    private void Reset()
    {
        score = 0;
        jumpIsReady = true;
        
        //Reset Movement and Position
        transform.position = startingPosition;
        rBody.velocity = Vector3.zero;
        
        OnReset?.Invoke();
    }

    private void OnCollisionEnter(Collision collidedObj)
    {
        if (collidedObj.gameObject.CompareTag("Street"))
            jumpIsReady = true;
        
        else if (collidedObj.gameObject.CompareTag("Mover") || collidedObj.gameObject.CompareTag("DoubleMover"))
            Reset();
    }

    private void OnTriggerEnter(Collider collidedObj)
    {
        if (collidedObj.gameObject.CompareTag("score"))
        {
            score++;
            ScoreCollector.Instance.AddScore(score);
        }
    }
}
