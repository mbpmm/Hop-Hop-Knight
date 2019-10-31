using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManager.Get().score.ToString();
        highScoreText.text =PlayerPrefs.GetInt("Highscore").ToString();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
        
    }

    public void GoToMenu()
    {
        
        SceneManager.LoadScene("IntroScene");
        GameManager.Get().score = 0;
    }

    public void ResetScore()
    {
        GameManager.Get().score = 0;
    }
}
