using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speedClose;
    public float speedOpen;
    public bool moving=true;
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
            AkSoundEngine.PostEvent("trap_platform_close", gameObject);
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

        if (gameObject.activeInHierarchy==false)
        {
            moving = true;
            stop = false;
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
                AkSoundEngine.PostEvent("trap_platform_open", gameObject);
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

    private void OnEnable()
    {
        stop = false;
        moving = true;
        finishMovement = false;
    }
}
