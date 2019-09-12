﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    Rigidbody2D rbody;
    Vector2 startpos;
    Vector2 endpos;
    Vector2 mousePos;
    public Vector2 direction;
    public float power = 5f; // power of shot
    public float distance;
    public float timeStart;
    public float timeEnd;
    public float timeInterval;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public bool launched;

    public GameObject mira;
    public GameObject miraVisual;
    float miraSize;
    float angle;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void MiraUpdate()
    {
        direction = startpos - endpos;

        angle = Vector3.Angle(direction, Vector3.up);

        miraVisual.transform.rotation = Quaternion.Euler(0,0,angle);
    }

    void Update()
    {
        MiraUpdate();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            launched = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            timeStart = Time.time;
            Debug.Log(startpos);
        }
        if (Input.GetMouseButtonUp(0) && !launched)
        {
            launched = true;
            endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            timeEnd = Time.time;
            timeInterval = timeEnd - timeStart;
            Debug.Log(endpos);
            LaunchPlayer();
        }
        //if (rbody.velocity.y < -0.57f)
        //{
        //    launched = false;
        //}

        Debug.Log("distancia: " + direction);
    }

    void LaunchPlayer()
    {
        direction = startpos - endpos;

        if (direction.y > 5f )
        {
            direction = new Vector2(direction.x, 5f);
        }

        if (direction.x < -6f)
        {
            direction = new Vector2(-6f, direction.y);
        }

        if (direction.x > 6f )
        {
            direction = new Vector2(6f, direction.y);
        }
        rbody.velocity = power*direction; // swap subtraction to switch direction of launch
        //rbody.AddForce(direction *  power);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemies")
        {
            transform.position = Vector2.zero;
            rbody.velocity = Vector2.zero;
        }
    }

    //public Vector2 startPos;
    //public Vector2 endPos;
    //public Vector2 direction;
    //public bool directionChosen;
    //public float distance;
    //public float touchTimeStart;
    //public float touchTimeFinish;
    //public float timeInterval;
    //Rigidbody2D rbody;

    //public float power = 0.3f; // power of shot
    //void Start()
    //{
    //    rbody = GetComponent<Rigidbody2D>();
    //}
    //void Update()
    //{
    //    // Track a single touch as a direction control.
    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    //    {
    //        touchTimeStart = Time.time;
    //        startPos = Input.GetTouch(0).position;
    //    }

    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
    //    {
    //        touchTimeFinish = Time.time;

    //        timeInterval = touchTimeFinish - touchTimeStart;
    //        endPos = Input.GetTouch(0).position;
    //        direction = startPos - endPos;
    //        rbody.AddForce((direction / timeInterval * power)/17f);
    //    }
    //}
}
