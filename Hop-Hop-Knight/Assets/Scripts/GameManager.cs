using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonobehaviourSingleton<GameManager>
{
    public int score;
    public Player player;
    public GameObject playerGO;
    // Start is called before the first frame update
    void Start()
    {
        Player.platformTouch += AddScore;
        Player.playerDeath += PlayerDied;
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
            AkSoundEngine.PostEvent("ui_ingame_highscore", gameObject);
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    IEnumerator LoadSceneAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("GameScene");
    }
}
