using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameManager;
    public int score;
    private GameManager gameMan;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameMan = gameManager.GetComponent<GameManager>();
        score = gameMan.score;
        scoreText.text = score.ToString();
    }
    

    void Update()
    {
        score = gameMan.score;
        scoreText.text = score.ToString();
    }
}
