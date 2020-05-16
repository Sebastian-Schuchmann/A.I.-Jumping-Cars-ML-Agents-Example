using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using UnityEngine;


/*
 * Singleton to globally collect the Scores of all Players.
 * When a new Highscore is reached, it is added to Tensorboard.
 */
public class ScoreCollector : MonoBehaviour
{
    public static ScoreCollector Instance; 

    [SerializeField] private TextMeshProUGUI display;
    
    private StatsRecorder statsRecorder;
    private int highScore = 0;
    void Awake()
    {
        Instance = this;
        statsRecorder = Academy.Instance.StatsRecorder;
    }

    public void AddScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            display.text = score.ToString();
            statsRecorder.Add("High Score", highScore, StatAggregationMethod.MostRecent);
        }

    }
}
