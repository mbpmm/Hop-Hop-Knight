using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speedClose;
    public float speedOpen;
    private bool moving=true;
    public bool stop;
    public bool finishMovement;
    public GameObject score;
    private StopPlatformMovement scoreScript;
    // Start is called before the first frame update
    void Start()
    {
        scoreScript = score.GetComponent<StopPlatformMovement>();
        scoreScript.movingPlatformTouch += StopMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x<-7f || transform.position.x > 7f)
        {
            moving = false;
        }


        if (moving&&!finishMovement)
        {
            transform.position += transform.right*-1f * Time.deltaTime * speedOpen;
        }
        else if(!finishMovement)
        {
            transform.position += transform.right* Time.deltaTime * speedClose;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TrapTrigger")
        {
            if (stop)
            {
                finishMovement = true;
            }
            else
            {
                moving = true;
            }
            
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
        }
    }

    public void StopMovement()
    {
        moving = false;
        stop = true;
    }
}
