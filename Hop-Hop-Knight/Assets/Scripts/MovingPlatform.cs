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

    public GameObject player;

    public float distancePlayer;
    public float aux1;
    public float aux2;
    private float maxValue = 16f;
    public float percentage;
    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        scoreScript = score.GetComponent<StopPlatformMovement>();
        scoreScript.movingPlatformTouch += StopMovement;
        aux1 = -15f;
        player = GameObject.FindGameObjectWithTag("Player");
        parent = GetComponentInParent<Transform>();
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

        distancePlayer = Vector2.Distance(player.transform.position, parent.transform.position);

        if (distancePlayer < 15f)
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
                AkSoundEngine.PostEvent("trap_platform_trigger", gameObject);
                finishMovement = true;
            }
            else
            {
                AkSoundEngine.PostEvent("trap_platform_open", gameObject);
                moving = true;
            }
            
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
