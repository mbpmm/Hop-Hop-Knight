using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Image[] powerBarLevel;
    public GameObject player;
    private Player2 playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player2>();
    }

    // Update is called once per frame
    void Update()
    {
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
        Destroy(GameObject.Find("GameManager"));
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
        Destroy(GameObject.Find("GameManager"));
    }
}
