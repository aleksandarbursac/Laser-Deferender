using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    TextMeshProUGUI scoreTextUI;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        scoreTextUI = GetComponent<TMPro.TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreTextUI.text = gameSession.ReturnScore().ToString();
    }
}
