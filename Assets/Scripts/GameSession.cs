using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    public void AddScore(int scoreWorth)
    {
        score += scoreWorth;
    }

    public string ReturnScore()
    {
        return score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
    }
}
