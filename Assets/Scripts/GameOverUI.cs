using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Jumper jumper;
    private void Awake()
    {
        jumper.OnReset += () => GetComponent<Animator>().SetTrigger("GameOver");
    }
}
