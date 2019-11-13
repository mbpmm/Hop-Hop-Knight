using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    public GameObject blockDestructionPoint;

    void Start()
    {
        blockDestructionPoint = GameObject.Find("BlockDestructionPoint");
    }

    void Update()
    {
        if (transform.position.y < blockDestructionPoint.transform.position.y)
        {
            gameObject.SetActive(false);
        }

    }
}
