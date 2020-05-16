using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Jumper : Agent
{
    public event Action OnReset;
    
    [SerializeField] private float jumpForce;
    [SerializeField] private KeyCode jumpKey;
    
    private bool jumpIsReady = true;
    private Rigidbody rigidbody;
    private Vector3 startingPosition;

    private int score = 0;

    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();
        startingPosition = transform.position;
    }
    
    public override void OnActionReceived(float[] vectorAction)
    {
        if (Mathf.FloorToInt(vectorAction[0]) == 1)
            Jump();
    }
    
    public override void OnEpisodeBegin()
    {
        score = 0;
        jumpIsReady = true;
        transform.position = startingPosition;
        rigidbody.velocity = Vector3.zero;
        OnReset?.Invoke();
    }
    
    public override void Heuristic(float[] actionsOut)
    {
        if (Input.GetKey(jumpKey))
        {
            actionsOut[0] = 1;
        }
        else 
        {
            //We have to reset actionsOut if we dont want constant jumping
            actionsOut[0] = 0;
        }
    }
    
    private void Jump()
    {
        if (jumpIsReady)
        {
            rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
            jumpIsReady = false;
        }
    }

    private void FixedUpdate()
    {
        if(jumpIsReady)
            RequestDecision();
    }

    private void OnCollisionEnter(Collision collidedObj)
    {
        if (collidedObj.gameObject.CompareTag("Street"))
        {
            jumpIsReady = true;
        }
        
        else if (collidedObj.gameObject.CompareTag("Mover") || collidedObj.gameObject.CompareTag("DoubleMover"))
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider collidedObj)
    {
        if (collidedObj.gameObject.CompareTag("score"))
        {
            score++;
            ScoreCollector.Instance.AddScore(score);
            AddReward(0.01f);
        }
    }
}
