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
    public bool throwOnce;
    public bool aux;
    public int score;
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
            DropArrow();
            throwOnce = true;
        }

        if (gameMan.score==20&&!aux)
        {
            throwOnce = false;
            aux = true;
        }
    }

    void DropArrow()
    {
        int rnd = UnityEngine.Random.Range(0, 3);
        GameObject arrowAux;

        if (rnd == 1)
            arrowAux = Instantiate(arrows, posCenter.transform.position, Quaternion.Euler(new Vector3(0,0,90)));
        else if(rnd==2)
            arrowAux = Instantiate(arrows, posLeft.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        else if (rnd == 0)
            arrowAux = Instantiate(arrows, posRight.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
    }
}
