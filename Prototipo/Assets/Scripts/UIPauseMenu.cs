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
    private Player2 playerScript;
    public Slider dragSlider;
    public Slider powerSlider;
    public Slider gravitySlider;
    public Text gravity;
    public Text drag;
    public Text power;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player2>();
        playerRB = player.GetComponent<Rigidbody2D>();
        dragSlider.value = playerRB.drag;
        powerSlider.value = playerScript.power;
        gravitySlider.value = playerRB.gravityScale;
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
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene("IntroScene");
        GameManager.Get().score = 0;
    }
    
}
