using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Image[] powerBarLevel;
    public GameObject player;
    private Rigidbody2D playerRB;
    private Player playerScript;
    public Slider dragSlider;
    public Slider powerSlider;
    public Slider gravitySlider;
    public Text gravity;
    public Text drag;
    public Text power;
    public Toggle music;
    public Toggle fx;

    public AK.Wwise.State PauseEnter;
    public AK.Wwise.State PauseExit;
    public AK.Wwise.State MuteFx;
    public AK.Wwise.State UnmuteFx;
    public AK.Wwise.State MuteMusic;
    public AK.Wwise.State UnmuteMusic;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        playerRB = player.GetComponent<Rigidbody2D>();
        dragSlider.value = playerRB.drag;
        powerSlider.value = playerScript.power;
        gravitySlider.value = playerRB.gravityScale;
        music.isOn = GameManager.Get().music;
        fx.isOn = GameManager.Get().fx;
    }

    // Update is called once per frame
    void Update()
    {
        playerRB.drag=dragSlider.value;
        playerScript.power=powerSlider.value;
        playerRB.gravityScale = gravitySlider.value;
        gravity.text = "GravityScale: " + playerRB.gravityScale;
        drag.text="Drag:"+ playerRB.drag;
        power.text = "Power: " + playerScript.power;

        for (int i = 0; i < powerBarLevel.Length; i++)
        {
            powerBarLevel[i].gameObject.SetActive(i < playerScript.cantGemas);
        }
    }

    public void Pause()
    {
        playerScript.onPause = true;
        PauseEnter.SetValue();
        AkSoundEngine.PostEvent("ui_pause_on", gameObject);
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }

    public void Unpause()
    {
        PauseExit.SetValue();
        AkSoundEngine.PostEvent("ui_pause_off", gameObject);
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        playerScript.onPause = false;
    }

    public void Menu()
    {
        AkSoundEngine.PostEvent("ui_menu_home", gameObject);
        AdsManager.Get().UIWatchAd();
        Player.platformTouch -= FindObjectOfType<CameraMovement>().Advance;
        SceneManager.LoadScene("IntroScene");
        GameManager.Get().score = 0;
    }
    
    public void MusicToggle(bool newValue)
    {
        GameManager.Get().music = newValue;
    }

    public void FxToggle(bool newValue)
    {
        GameManager.Get().fx = newValue;
    }
}
