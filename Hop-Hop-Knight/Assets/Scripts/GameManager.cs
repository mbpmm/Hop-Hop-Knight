using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;


public class GameManager : MonobehaviourSingleton<GameManager>
{
    public int score;
    public Player player;
    public GameObject playerGO;

    public bool music;
    public bool fx;
    public bool aux1;
    public bool aux2;
    public bool aux3;
    public bool aux4;

    public AK.Wwise.State MuteMusic;
    public AK.Wwise.State UnmuteMusic;
    public AK.Wwise.State MuteFx;
    public AK.Wwise.State UnmuteFx;
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

        if (music)
        {
            if (!aux1)
            {
                MuteMusic.SetValue();
                Debug.Log("Pase por mutemusic");
                aux1 = true;
                aux2 = false;
            }
            
        }
        else
        {
            if (!aux2)
            {
                UnmuteMusic.SetValue();
                Debug.Log("Pase por unmutemusic");
                aux2 = true;
                aux1 = false;
            }
            
        }

        if (fx)
        {
            if (!aux3)
            {
                MuteFx.SetValue();
                Debug.Log("Pase por mutefx");
                aux3 = true;
                aux4 = false;
            }
            
        }
        else
        {
            if (!aux4)
            {
                UnmuteFx.SetValue();
                Debug.Log("Pase por unmutefx");
                aux4 = true;
                aux3 = false;
            }
            
        }
    }

    void AddScore()
    {
        score++;
    }

    void AddScorePowerUp()
    {
        score+=2;
    }

    void PlayerSetter(Player go)
    {
        player = go;
        playerGO = go.gameObject;
    }

    void PlayerDied()
    {
        ScorePerSession(score);
        if (score>PlayerPrefs.GetInt("Highscore",0))
        {
            Highscore(score);
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

    public void ScorePerSession(int score)
    {
        Analytics.CustomEvent("session_finished", new Dictionary<string, object>
        {
            {"Score",score}
        });
    }

    public void Highscore(int score)
    {
        Analytics.CustomEvent("highscore_achieved", new Dictionary<string, object>
        {
            {"Highscore",score}
        });
    }
}
