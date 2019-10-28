using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    public GameObject blockDestructionPoint;
    public GameObject scoreZone;
    // Start is called before the first frame update
    void Start()
    {
        blockDestructionPoint = GameObject.Find("BlockDestructionPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y<blockDestructionPoint.transform.position.y)
        {
            gameObject.SetActive(false);
            scoreZone.SetActive(true);
        }
        
    }
}
