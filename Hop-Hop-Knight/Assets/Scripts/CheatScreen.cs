using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheatScreen : MonoBehaviour
{
    public GameObject cheatScreen;
    public GameObject player;
    private Player playerScript;
    public Toggle invulnerabilidad;
    private int timesTouched;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timesTouched==3)
        {
            OpenCheats();
        }
        if (invulnerabilidad.isOn)
        {
            playerScript.powerUpActivated = true;
        }
        else if (!invulnerabilidad.isOn)
        {
            //playerScript.powerUpActivated = false;
        }
    }

    public void CheatButtonClicked()
    {
        timesTouched++;
    }
    public void OpenCheats()
    {
        Time.timeScale = 0;
        cheatScreen.gameObject.SetActive(true);
        timesTouched = 0;
    }

    public void CloseCheats()
    {
        Time.timeScale = 1;
        cheatScreen.gameObject.SetActive(false);
    }

    public void GoToLevel5()
    {
        player.transform.position = new Vector3(4, 38, 0);
    }
    public void GoToLevel10()
    {
        player.transform.position = new Vector3(-4, 77, 0);
    }
    public void GoToLevel15()
    {
        player.transform.position = new Vector3(0, 121, 0);
    }
    public void GoToLevel20()
    {
        player.transform.position = new Vector3(0, 158, 0);
    }
}