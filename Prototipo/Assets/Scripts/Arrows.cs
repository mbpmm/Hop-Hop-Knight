using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject posCenter;
    public GameObject posLeft;
    public GameObject posRight;
    public GameObject arrows;
    public GameObject posCenterWarning;
    public GameObject posLeftWarning;
    public GameObject posRightWarning;
    public GameObject warningSign;
    private GameObject warningAux;
    public float warningTime;
    public bool throwOnce;
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
        if (gameMan.score%10==0&&gameMan.score!=0&&!throwOnce)
        {
            rnd = UnityEngine.Random.Range(0, 3);
            ShowSign();
            Invoke("DropArrow", warningTime);
            //DropArrow();
            throwOnce = true;
        }
        else if (gameMan.score % 10 != 0&&throwOnce)
        {
            throwOnce = false;
        }
    }

    void DropArrow()
    {
        GameObject arrowAux;

        if (rnd == 1)
            arrowAux = Instantiate(arrows, posCenter.transform.position, Quaternion.Euler(new Vector3(0,0,90)));
        else if(rnd==2)
            arrowAux = Instantiate(arrows, posLeft.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        else if (rnd == 0)
            arrowAux = Instantiate(arrows, posRight.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
    }

    void ShowSign()
    {
        if (rnd == 1)
        {
            warningAux = Instantiate(warningSign, posCenterWarning.transform.position, Quaternion.identity);
            warningAux.transform.SetParent(posCenterWarning.transform);
        }
        else if (rnd == 2)
        {
            warningAux = Instantiate(warningSign, posLeftWarning.transform.position, Quaternion.identity);
            warningAux.transform.SetParent(posLeftWarning.transform);
        }
        else if (rnd == 0)
        {
            warningAux = Instantiate(warningSign, posRightWarning.transform.position, Quaternion.identity);
            warningAux.transform.SetParent(posRightWarning.transform);
        }

        Destroy(warningAux, warningTime);
            
    }
}
