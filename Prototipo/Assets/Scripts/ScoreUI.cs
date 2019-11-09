using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;

    void Start()
    {
        score = GameManager.Get().score;
        scoreText.text = score.ToString();
    }
    

    void Update()
    {
        score = GameManager.Get().score;
        scoreText.text = score.ToString();
    }
}
