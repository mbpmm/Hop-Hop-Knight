using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public float vertical;
    public float horizontal;
    public GameObject bullet;
    public GameObject bulletPU;
    public GameObject missile;
    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public float fireRate;
    public bool exitOnce;

    private float powerUpDuration;
    private bool powerUp;
    private Rigidbody2D playerRB;
    private float limMax, limXMin, limYMin;
    private float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        limMax = 0.96f;
        limXMin = 0.04f;
        limYMin = 0.07f;
        exitOnce = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, vertical, 0);

        playerRB.velocity = movement * speed;
        

        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            
        }
        if (Input.GetKeyDown(KeyCode.Space) )
        {

        }

        if (powerUp)
        {
            powerUpDuration += Time.deltaTime;
            if (powerUpDuration > 8f)
            {
                powerUp = false;
                powerUpDuration = 0f;
            }
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (exitOnce)
        {
            if (collision.tag == "room1")
            {
                room1.gameObject.SetActive(false);
                room2.gameObject.SetActive(true);
            }

            if (collision.tag == "room2")
            {
                room1.gameObject.SetActive(true);
                room2.gameObject.SetActive(false);
            }
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.tag == "room1")
    //    {
    //        exitOnce = false;
    //    }

    //    if (other.tag == "room2")
    //    {
    //        exitOnce = true;
    //    }
        
    //}
}
