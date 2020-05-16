using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Jumper : Agent
{
    [SerializeField] private float jumpForce;
    [SerializeField] private KeyCode jumpKey;
    
    private bool jumpIsReady = true;
    private Vector3 startingPosition;
    private StatsRecorder statsRecorder;
    
    private int score = 0;
    private int highScore = 0;
    
    public event Action OnReset;


    public override void Initialize()
    {
        statsRecorder = Academy.Instance.StatsRecorder;
        startingPosition = transform.position;
    }


    public override void OnActionReceived(float[] vectorAction)
    {
        var action = Mathf.FloorToInt(vectorAction[0]);

        if (action == 1)
        {
            Jump();
        }
    }
    
    public override void OnEpisodeBegin()
    {
        RecordScore();


        score = 0;
        jumpIsReady = true;
        transform.position = startingPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void RecordScore()
    {
        statsRecorder.Add("score", score);
    }


    public override void Heuristic(float[] actionsOut)
    {
        if (Input.GetKey(jumpKey))
        {
            actionsOut[0] = 1;
        }
        else //We have to reset actionsOut if we dont want constant jumping
        {
            actionsOut[0] = 0;
        }
    }
    
    private void Jump()
    {
        if (jumpIsReady)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
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
            OnReset?.Invoke();
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
