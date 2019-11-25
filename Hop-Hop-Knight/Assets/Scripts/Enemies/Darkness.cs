using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public Transform startPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Get().score != 0)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }

        if (transform.position.y < startPos.position.y)
        {
            transform.position = startPos.position;
        }
    }
}
