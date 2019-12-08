﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBroken : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            anim.SetTrigger("PlayerEnter");
        }
    }

    private void OnDisable()
    {
        anim.SetTrigger("GoToIdle");
    }
}
