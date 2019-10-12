using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    public float speed;
    public GameObject player;

    public Rigidbody2D playerRB;
    public Transform startPos;

    public GameObject gameManager;
    private GameManager gameMan;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager");
        gameMan = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = player.transform.position;
        if (playerRB.velocity.y==0&&gameMan.score!=0)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }
        else
        {
            //transform.position -= transform.up * Time.deltaTime * speed* playerRB.velocity.y;
        }

        if (transform.position.y<startPos.position.y)
        {
            transform.position = startPos.position;
        }
        
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector3(0, -18f, 0);
    }
}
