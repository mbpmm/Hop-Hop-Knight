using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrap : MonoBehaviour
{
    public float speed;
    private bool moving=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x<-8f || transform.position.x > 8f)
        {
            moving = false;
        }


        if (moving)
        {
            transform.position += transform.right*-1f * Time.deltaTime * speed;
        }
        else
        {
            transform.position += transform.right* Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TrapTrigger")
        {
            moving = true;
            Debug.Log("que pasa aca amigo");
        }
    }
}
