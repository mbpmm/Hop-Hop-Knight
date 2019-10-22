using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    Animator anim;
    public float fireRate;
    private float nextShoot;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + fireRate;
            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }
}
