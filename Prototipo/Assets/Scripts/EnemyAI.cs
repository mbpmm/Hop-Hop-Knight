﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health;
    public float speed;
    public float distance;
    public bool movingRight = true;
    public Transform groundDetection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider==false)
        {
            if (movingRight==true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        if (health<=0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ChangeColor", 0.1f);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void ChangeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

}
