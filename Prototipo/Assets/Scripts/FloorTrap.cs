using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrap : MonoBehaviour
{
    public float speedClose;
    public float speedOpen;
    private bool moving=true;
    public bool stop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x<-7f || transform.position.x > 7f)
        {
            moving = false;
        }


        if (moving&&!stop)
        {
            transform.position += transform.right*-1f * Time.deltaTime * speedClose;
        }
        else if(!stop)
        {
            transform.position += transform.right* Time.deltaTime * speedOpen;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TrapTrigger")
        {
            moving = true;
        }

        if (collision.gameObject.tag == "Score")
        {
            moving = true;
            stop = true;
            transform.position = transform.position;
        }
    }
}
