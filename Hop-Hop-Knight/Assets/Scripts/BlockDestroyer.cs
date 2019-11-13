using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    public GameObject blockDestructionPoint;
    public GameObject scoreZone;

    void Start()
    {
        blockDestructionPoint = GameObject.Find("BlockDestructionPoint");
    }
    
    void Update()
    {
        if (transform.position.y<blockDestructionPoint.transform.position.y)
        {
            gameObject.SetActive(false);
            if (scoreZone)
            {
                scoreZone.SetActive(true);
            }
        }
        
    }
}
