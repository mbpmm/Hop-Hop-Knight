using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject block;
    public Transform generationPoint;
    public float randomBlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y<generationPoint.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z);
            randomBlock = Random.Range(1, 15);
            GameObject go = ObjectPool.instance.GetPooledObject(randomBlock.ToString());

            go.transform.position =transform.position;
        }
    }
}
