using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speedClose;
    public float speedOpen;
    public float maxPosition;
    public float minPosition;
    public float distanceMax;
    public bool moving=true;
    public bool stop;
    public bool finishMovement;
    public GameObject score;
    private StopPlatformMovement scoreScript;

    public GameObject player;

    public float distancePlayer;
    public float aux1;
    public float aux2;
    private float maxValue = 16f;
    public float percentage;
    // Start is called before the first frame update
    void Start()
    {
        scoreScript = score.GetComponent<StopPlatformMovement>();
        scoreScript.movingPlatformTouch += StopMovement;
        aux1 = -distanceMax;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < minPosition || transform.position.x > maxPosition)
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

        distancePlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distancePlayer < distanceMax)
        {
            aux2 = Mathf.Abs(aux1 + distancePlayer);
            percentage = (aux2 * 100f) / maxValue;
            Mathf.Clamp(percentage, 0, 100);
            AkSoundEngine.SetRTPCValue("distance_trap_moving_platform", percentage);

        }
        else
        {
            percentage = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TrapTrigger")
        {
            if (stop)
            {
                AkSoundEngine.PostEvent("trap_platform_close", gameObject);
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
