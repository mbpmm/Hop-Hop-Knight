using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potty : MonoBehaviour
{
    public GameObject bulletEmitter;
    public GameObject bullet;
    public float fireRate;
    private float nextShoot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bulletAux;
        bulletAux = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation);
    }
}
