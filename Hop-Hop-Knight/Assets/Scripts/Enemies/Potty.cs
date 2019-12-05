using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potty : MonoBehaviour
{
    public GameObject bulletEmitter;
    public GameObject bullet;
    void Shoot()
    {
        AkSoundEngine.PostEvent("enemy_plant", gameObject);
        GameObject go = ObjectPool.instance.GetPooledObject("Bullet");
        go.transform.position = bulletEmitter.transform.position;
        go.transform.rotation = bulletEmitter.transform.rotation;
    }
}
