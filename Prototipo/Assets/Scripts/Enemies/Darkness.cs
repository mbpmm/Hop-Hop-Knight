using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    public float speed;
    public GameObject player;

    public Player2 playerRB;
    public Transform startPos;

    public GameObject gameManager;
    private GameManager gameMan;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = player.GetComponent<Player2>();
        gameManager = GameObject.Find("GameManager");
        gameMan = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMan.score != 0)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }

        if (transform.position.y < startPos.position.y)
        {
            transform.position = startPos.position;
        }
    }
}
