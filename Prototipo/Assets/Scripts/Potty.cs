using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potty : MonoBehaviour
{
    public GameObject bulletEmitter;
    public GameObject bullet;
    void Shoot()
    {
        GameObject bulletAux;
        bulletAux = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation);
    }
}
