using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject arrows;
    public GameObject warningSign;
    private GameObject warningAux;
    public GameObject[] positions;
    public GameObject[] warningPositions;

    public float warningTime;
    private int lastLevelSpikes;
    public bool aux;
    public int score;
    private int rnd;
    private GameManager gameMan;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameMan = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMan.score%10==0 && gameMan.score != lastLevelSpikes)
        {
            lastLevelSpikes = gameMan.score;
            rnd = UnityEngine.Random.Range(0, 3);
            ShowSign();
            Invoke("DropArrow", warningTime-0.1f);
        }
    }

    void DropArrow()
    {
        GameObject arrowAux;
        arrowAux = Instantiate(arrows, positions[rnd].transform.position, Quaternion.Euler(new Vector3(0,0,90)));
        AkSoundEngine.PostEvent("trap_arrows", gameObject);
    }

    void ShowSign()
    {
        warningAux = Instantiate(warningSign, warningPositions[rnd].transform.position, Quaternion.identity);
        warningAux.transform.SetParent(positions[rnd].transform);
        AkSoundEngine.PostEvent("ui_ingame_warning", gameObject);

        Destroy(warningAux, warningTime);
    }
}
