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
    void OnEnable()
    {
        Player.platformTouch += AddScore;
        Player.playerDeath += PlayerDied;
        Player.playerStarted += PlayerSetter;
        Player.powerUpScore += AddScorePowerUp;
    }
    private void Update()
    {
        AkSoundEngine.SetRTPCValue("floor_number", score);
    }

    void AddScore()
    {
        score++;
    }

    void AddScorePowerUp()
    {
        score+=10;
    }

    void PlayerSetter(Player go)
    {
        player = go;
        playerGO = go.gameObject;
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

    private void OnDisable()
    {
        Player.platformTouch -= AddScore;
        Player.playerDeath -= PlayerDied;
        Player.playerStarted -= PlayerSetter;
        Player.powerUpScore -= AddScorePowerUp;
    }
}
