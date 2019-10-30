using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonobehaviourSingleton<GameManager>
{
    public int score;
    public Player2 player;
    public GameObject deathScreen;
    private Animator deathAnim;

    // Start is called before the first frame update
    void Start()
    {
        Player2.platformTouch += AddScore;
        Player2.playerDeath += PlayerDied;
        deathScreen = GameObject.Find("SceneFade");
        deathAnim = deathScreen.GetComponent<Animator>();
    }
    private void Update()
    {
        
    }

    void AddScore()
    {
        score++;
    }

    void PlayerDied()
    {
        if (score>PlayerPrefs.GetInt("Highscore",0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    IEnumerator LoadSceneAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("GameScene");
    }
}
