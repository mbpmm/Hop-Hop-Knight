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
        
        AkSoundEngine.PostEvent("ui_menu_play_again", gameObject);
        AdsManager.Get().UIWatchAd();
        SceneManager.LoadScene("GameScene");
        
    }

    public void GoToMenu()
    {
        AkSoundEngine.PostEvent("ui_menu_home", gameObject);
        AdsManager.Get().UIWatchAd();
        SceneManager.LoadScene("IntroScene");
        GameManager.Get().score = 0;
    }

    public void ResetScore()
    {
        GameManager.Get().score = 0;
    }
}
