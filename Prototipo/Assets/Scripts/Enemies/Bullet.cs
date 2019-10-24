using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeSpan;
    private float timer;
    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        transform.position += transform.right * Time.deltaTime * speed;

        if (timer >= lifeSpan)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
